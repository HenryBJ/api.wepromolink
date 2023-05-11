using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class PricingController : ControllerBase
{

    private readonly IPricingService _service;

    public PricingController(IPricingService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("all")]
    [ResponseCache(Duration = 1800)] // duración en segundos (30 minutos)
    public async Task<IResult> GetAll()
    {
        try
        {
            var results = await _service.GetAll();
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }
    
}
