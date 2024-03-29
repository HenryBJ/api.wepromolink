using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UserController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(IUserService service, ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Route("exits/{email}")]
    public async Task<IActionResult> Exits(string email)
    {
        try
        {
            var results = await _service.Exits(email);
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Route("isblocked")]
    [Authorize]
    [ResponseCache(Duration = 600)]
    public async Task<IActionResult> IsBlocked()
    {
        try
        {
            var results = await _service.IsBlocked();
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Route("payment/methods")]
    [Authorize]
    public async Task<IActionResult> GetPaymentMethods()
    {
        try
        {
            var results = await _service.GetPaymentMethods();
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Route("issubscribed")]
    [Authorize]
    public async Task<IActionResult> IsSubscribed()
    {
        try
        {
            var results = await _service.IsSubscribed();
            if (results)
            {
                _httpContextAccessor.HttpContext!.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromHours(3)
                };
            }
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    [Route("firebaseuid")]
    public async Task<IActionResult> SetFirebaseUid([FromBody] dynamic request)
    {
        try
        {
            string email = request.GetProperty("email").GetString();
            string uid = request.GetProperty("uid").GetString();
            await _service.SetFirebaseUid(email, uid);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpData data)
    {
        try
        {
            var response = await _service.SignUp(data);
            return new OkObjectResult(response);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
