using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using WePromoLink.DTO;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private const string DATABASE = "wepromolink";
    private readonly IDataService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<DataController> _logger;
    private readonly IMongoClient _client;

    public DataController(IHttpContextAccessor httpContextAccessor, IDataService service, ILogger<DataController> logger, IMongoClient client)
    {
        _httpContextAccessor = httpContextAccessor;
        _service = service;
        _logger = logger;
        _client = client;
    }

    [HttpGet("{collectionName}/{externalId}")]
    [Authorize]
    public IActionResult Get(string collectionName, string externalId)
    {
        var database = _client.GetDatabase(DATABASE);
        var collection = database.GetCollection<BsonDocument>(collectionName);

        var filter = Builders<BsonDocument>.Filter.Eq("_id", externalId);
        var document = collection.Find(filter).FirstOrDefault();

        if (document == null)
        {
            return NotFound();
        }
        var json = document.ToJson();
        _logger.LogInformation(json);
        switch (collectionName)
        {
            case StatisticsEnum.CampaignXClick:
                return new OkObjectResult(Newtonsoft.Json.JsonConvert.DeserializeObject<ChartData<string, int>>(json));
            default:
                return Ok();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("available")]
    public async Task<IActionResult> GetAvailable()
    {
        try
        {
            return await _service.GetAvailable();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("budget")]
    public async Task<IActionResult> GetBudget()
    {
        try
        {
            return await _service.GetBudget();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("profit")]
    public async Task<IActionResult> GetProfit()
    {
        try
        {
            return await _service.GetProfit();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
