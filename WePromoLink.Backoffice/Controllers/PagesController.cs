
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO.StaticPage;
using WePromoLink.DTO.SubscriptionPlan;
using WePromoLink.Services;
using WePromoLink.Services.StaticPages;
using WePromoLink.Services.SubscriptionPlan;

namespace WePromoLink.Backoffice.Controllers;

[Route("[controller]")]
[ApiController]
public class PagesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IStaticPageService _service;
    private readonly ILogger<PagesController> _logger;

    public PagesController(IConfiguration configuration, IStaticPageService service, ILogger<PagesController> logger)
    {
        _configuration = configuration;
        _service = service;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("getAll/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAll(int? page, int? cant, string? filter = "")
    {
        try
        {
            var result = await _service.GetAllStaticPage(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var result = await _service.GetStaticPage(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(StaticPageCreate data)
    {
        try
        {
            var result = await _service.CreateStaticPage(data);
            return new CreatedResult($"/pages/get/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("add/product/{pageId}/{productId}")]
    public async Task<IActionResult> AddProduct(Guid? pageId, Guid? productId)
    {
        try
        {
            await _service.AddProduct(pageId.Value, productId.Value);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("clearcache/{pageId}")]
    public async Task<IActionResult> ClearCache(Guid? pageId)
    {
        try
        {
            await _service.ClearCache(pageId!.Value);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("remove/product/{pageId}/{productId}")]
    public async Task<IActionResult> RemoveProduct(Guid? pageId, Guid? productId)
    {
        try
        {
            await _service.RemoveProduct(pageId.Value, productId.Value);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit(StaticPageEdit data)
    {
        try
        {
            await _service.EditStaticPage(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteStaticPage(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // DataTemplates

    [Authorize]
    [HttpGet("data/getAll/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAllDataTemplate(int? page, int? cant, string? filter)
    {
        try
        {
            var result = await _service.GetAllStaticPageDataTemplate(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("data/get/{id}")]
    public async Task<IActionResult> GetDataTemplate(Guid id)
    {
        try
        {
            var result = await _service.GetStaticPageDataTemplate(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("data/create")]
    public async Task<IActionResult> CreateDataTemplate(IFormFile file, [FromForm] string name)
    {
        try
        {
            var result = await _service.CreateStaticPageDataTemplate(new StaticPageDataTemplateCreate { File = file, Name = name });
            return new CreatedResult($"/pages/data/get/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPut("data/edit")]
    public async Task<IActionResult> EditDataTemplate(IFormFile file, [FromForm] string name, [FromForm] Guid id)
    {
        try
        {
            await _service.EditStaticPageDataTemplate(new StaticPageDataTemplateEdit { File = file, Id = id, Name = name });
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("data/delete/{id}")]
    public async Task<IActionResult> DeleteDataTemplate(Guid id)
    {
        try
        {
            await _service.DeleteStaticPageDataTemplate(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // WebsiteTemplate

    [Authorize]
    [HttpGet("website/getAll/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAllWebsiteTemplate(int? page, int? cant, string? filter)
    {
        try
        {
            var result = await _service.GetAllStaticPageWebsiteTemplate(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("website/get/{id}")]
    public async Task<IActionResult> GetwebsiteTemplate(Guid id)
    {
        try
        {
            var result = await _service.GetStaticPageWebsiteTemplate(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("website/create")]
    public async Task<IActionResult> CreateWebsiteTemplate(IFormFile file, [FromForm] string name)
    {
        try
        {
            var result = await _service.CreateStaticPageWebsiteTemplate(new StaticPageWebsiteTemplateCreate { File = file, Name = name });
            return new CreatedResult($"/pages/website/get/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPut("website/edit")]
    public async Task<IActionResult> EditWebsiteTemplate(IFormFile file, [FromForm] string name, [FromForm] Guid id)
    {
        try
        {
            await _service.EditStaticPageWebsiteTemplate(new StaticPageWebsiteTemplateEdit { File = file, Id = id, Name = name });
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("website/delete/{id}")]
    public async Task<IActionResult> DeleteWebsiteTemplate(Guid id)
    {
        try
        {
            await _service.DeleteStaticPageWebsiteTemplate(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // Resources

    [Authorize]
    [HttpGet("resource/getAll/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAllResource(int? page, int? cant, string? filter)
    {
        try
        {
            var result = await _service.GetAllStaticPageResource(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("resource/get/{id}")]
    public async Task<IActionResult> GetResource(Guid id)
    {
        try
        {
            var result = await _service.GetStaticPageResource(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("resource/create")]
    public async Task<IActionResult> CreateResource(IFormFile file, [FromForm] string name, [FromForm] string contentType, [FromForm] decimal sizeMB, [FromForm] int? height, [FromForm] int? width)
    {
        try
        {
            var result = await _service.CreateStaticPageResource(new StaticPageResourceCreate
            {
                ContentType = contentType,
                File = file,
                Height = height,
                Name = name,
                SizeMB = sizeMB,
                Width = width
            });
            return new CreatedResult($"/pages/resource/get/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPut("resource/edit")]
    public async Task<IActionResult> EditResource(IFormFile file, [FromForm] Guid id, [FromForm] string name, [FromForm] string contentType, [FromForm] decimal sizeMB, [FromForm] int? height, [FromForm] int? width)
    {
        try
        {
            await _service.EditStaticPageResource(new StaticPageResourceEdit
            {
                Id = id,
                ContentType = contentType,
                File = file,
                Height = height,
                Name = name,
                SizeMB = sizeMB,
                Width = width

            });
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("resource/delete/{id}")]
    public async Task<IActionResult> DeleteResource(Guid id)
    {
        try
        {
            await _service.DeleteStaticPageResource(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // Products

    [Authorize]
    [HttpGet("products/getAll/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAllProducts(int? page, int? cant, string? filter)
    {
        try
        {
            var result = await _service.GetAllStaticPageProduct(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("products/get/{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        try
        {
            var result = await _service.GetStaticPageProduct(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("products/create")]
    public async Task<IActionResult> CreateProduct(StaticPageProductCreate data)
    {
        try
        {
            var result = await _service.CreateStaticPageProduct(data);
            return new CreatedResult($"/pages/products/get/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    
    [Authorize]
    [HttpPut("products/edit")]
    public async Task<IActionResult> EditProduct(StaticPageProductEdit data)
    {
        try
        {
            await _service.EditStaticPageProduct(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("products/delete/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            await _service.DeleteStaticPageProduct(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("products/add/resource/{productId}/{resourceId}")]
    public async Task<IActionResult> AddResource(Guid? productId, Guid? resourceId)
    {
        try
        {
            await _service.AddResource(productId.Value, resourceId.Value);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("products/remove/resource/{productId}/{resourceId}")]
    public async Task<IActionResult> RemoveResource(Guid? productId, Guid? resourceId)
    {
        try
        {
            await _service.RemoveResource(productId.Value, resourceId.Value);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // Product per Page (many to many)

    [Authorize]
    [HttpPost("productsbypage/create")]
    public async Task<IActionResult> CreateProductByPage(StaticPageProductByPageCreate data)
    {
        try
        {
            var result = await _service.CreateStaticPageProductByPage(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("productsbypage/delete/{id}")]
    public async Task<IActionResult> DeleteProductByPage(Guid id)
    {
        try
        {
            await _service.DeleteStaticPageProductByPage(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("productsbypage/exits/{pageId}/{productId}")]
    public async Task<IActionResult> ExitsProductByPage(Guid? pageId, Guid? productId)
    {
        try
        {
            var result = await _service.ExitsStaticPageProductByPage(new StaticPageProductByPageCreate
            {
                StaticPagePageModelId = pageId,
                StaticPageProductModelId = productId
            });

            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // Product per Resource (many to many)

    [Authorize]
    [HttpPost("productsbyresource/create")]
    public async Task<IActionResult> CreateProductByResource(StaticPageProductByResourceCreate data)
    {
        try
        {
            var result = await _service.CreateStaticPageProductByResource(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("productsbyresource/delete/{id}")]
    public async Task<IActionResult> DeleteProductByResource(Guid id)
    {
        try
        {
            await _service.DeleteStaticPageProductByResource(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("productsbyresource/exits/{productId}/{resourceId}")]
    public async Task<IActionResult> ExitsProductByResource(Guid? productId, Guid? resourceId)
    {
        try
        {
            var result = await _service.ExitsStaticPageProductByResource(new StaticPageProductByResourceCreate
            {
                StaticPageProductModelId = productId,
                StaticPageResourceModelId = resourceId
            });
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


}
