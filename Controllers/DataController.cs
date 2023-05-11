using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Filters;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DataController(IHttpContextAccessor httpContextAccessor, IDataService service)
    {
        _httpContextAccessor = httpContextAccessor;
        _service = service;
    }


    [HttpGet]
    [Authorize]
    [Route("available")]
    [DynamicCache(resultType:"decimal")]
    public async Task<IResult> GetAvailable()
    {
        try
        {
            var results = await _service.GetAvailable();
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }
    
}
