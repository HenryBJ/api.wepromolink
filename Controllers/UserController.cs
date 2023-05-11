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

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("exits/{email}")]
    public async Task<IResult> Exits(string email)
    {
        try
        {
            var results = await _service.Exits(email);
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Route("isblocked")]
    [Authorize]
    [ResponseCache(Duration = 600)]
    public async Task<IResult> IsBlocked()
    {
        try
        {
            var results = await _service.IsBlocked();
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Route("issubscribed")]
    [Authorize]
    [ResponseCache(Duration = 60)]
    public async Task<IResult> IsSubscribed()
    {
        try
        {
            var results = await _service.IsSubscribed();
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpPut]
    [Route("firebaseuid")]
    public async Task<IResult> SetFirebaseUid([FromBody] dynamic request)
    {
        try
        {
            string email = request.GetProperty("email").GetString();
            string uid = request.GetProperty("uid").GetString();
            await _service.SetFirebaseUid(email, uid);
            return Results.Ok();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }


    [HttpPost]
    [Route("signup")]
    public async Task<IResult> SignUp([FromBody] SignUpData data)
    {
        try
        {
            var response = await _service.SignUp(data);
            return Results.Ok(response);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

}
