using System.Net;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scriban;
using Scriban.Runtime;
using WePromoLink.Data;
using WePromoLink.DTO.StaticPage;
using WePromoLink.Services.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<BlobServiceClient>(_ =>
{
    return new BlobServiceClient(builder.Configuration["Azure:blob:connectionstring"]);
});
builder.Services.AddSingleton<IShareCache>(x =>
{
    return new RedisCache(
        builder.Configuration["Redis:Host"],
        builder.Configuration["Redis:Port"],
        builder.Configuration["Redis:Password"]);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/favicon.ico", async (HttpContext httpContext) =>
{

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();

    var subdomain = httpContext.Request.Host.Host;
    // var subdomain = "demo.wepromolink.com";

    var pageId = await db.StaticPages
    .Where(e => e.Name.ToLower() == subdomain)
    .Select(e => e.Id)
    .SingleOrDefaultAsync();
    if (pageId == Guid.Empty)
    {
        httpContext.Response.StatusCode = 404;
        await httpContext.Response.WriteAsync("Favicon not found, page not found");
        return;
    }

    var fav = db.StaticPageResources
        .Where(e => e.ContentType == "image/x-icon" && e.Name.ToLower() == subdomain)
        .FirstOrDefault();

    if (fav == null)
    {
        httpContext.Response.StatusCode = 404;
        await httpContext.Response.WriteAsync("Favicon not found");
        return;
    }
    byte[] faviconData = await DownloadFavIconAsync(fav.Url);
    if (faviconData.Length == 0)
    {
        httpContext.Response.StatusCode = 404;
        await httpContext.Response.WriteAsync("Favicon not found");
        return;
    }

    var cacheControl = new CacheControlHeaderValue
    {
        MaxAge = TimeSpan.FromHours(6), // Establece el tiempo de caché a 30 minutos
        Public = true,
    };

    httpContext.Response.GetTypedHeaders().CacheControl = cacheControl;
    await httpContext.Response.Body.WriteAsync(faviconData);
});


app.MapGet("/", async httpContext =>
{
    var subdomain = httpContext.Request.Host.Host;
    // var subdomain = "demo.wepromolink.com";
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    var cache = scope.ServiceProvider.GetRequiredService<IShareCache>();

    if (httpContext.Request.Headers.ContainsKey("If-None-Match"))
    {
        var _etag = httpContext.Request.Headers["If-None-Match"];
        if (cache.TryGetValue($"page_etag_{subdomain}", out string? cachePageEtag))
        {
            if (!string.IsNullOrEmpty(cachePageEtag) && _etag == cachePageEtag)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotModified;
                return;
            }
        }
    }

    if (cache.TryGetValue($"page_{subdomain}", out string? cachePage))
    {
        if (!string.IsNullOrEmpty(cachePage))
        {
            httpContext.Response.ContentType = "text/html";
            httpContext.Response.Headers.ETag = cache.Get<string>($"page_etag_{subdomain}");
            await httpContext.Response.WriteAsync(cachePage);
            return;
        }
    }

    var defaultResponseHtml = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <title>WePromoLink Static Page</title>
        </head>
        <body>
            <h1>Welcome to WePromoLink's Static Page</h1>
            <p>Current Date: {DateTime.Now.ToShortDateString()}</p>
            <p>Subdomain: {subdomain}</p>
            <p>Thank you for visiting our static page!</p>
            <p>If you're looking for more information, please visit our website:</p>
            <a href='https://wepromolink.com'>Visit WePromoLink</a>
        </body>
        </html>
    ";

    var page = await db.StaticPages
    .Include(e => e.StaticPageDataTemplate)
    .Include(e => e.StaticPageWebsiteTemplate)
    .Where(e => e.Name.ToLower() == subdomain)
    .SingleOrDefaultAsync();

    var products = await db.StaticPageProductByPages
    .Include(e => e.Product)
    .Where(e => e.StaticPageModelId == page.Id)
    .Select(e=> new StaticPageProductRead
    {
        Id = e.Product.Id,
        AffiliateLink = e.Product.AffiliateLink,
        AffiliateProgram = e.Product.AffiliateProgram,
        BuyLink = e.Product.BuyLink,
        Category = e.Product.Category,
        Commission = e.Product.Commission,
        CostPrice = e.Product.CostPrice,
        Description = e.Product.Description,
        Discount = e.Product.Discount,
        Height = e.Product.Height,
        Inventory = e.Product.Inventory,
        Length = e.Product.Length,
        Price = e.Product.Price,
        Provider = e.Product.Provider,
        SKU = e.Product.SKU,
        Tags = e.Product.Tags,
        Title = e.Product.Title,
        Weight = e.Product.Weight,
        Width = e.Product.Width

    })
    .ToListAsync();

    foreach (var product in products)
    {
        product.ImagesUrl = await db.StaticPageProductByResources.Where(e=>e.StaticPageProductModelId == product.Id).Select(e=>e.Resource.Url).ToListAsync();
    }

    if (page == null)
    {
        await httpContext.Response.WriteAsync(defaultResponseHtml);
        return;
    }

    var dic = await DownloadJsonAsync(page.StaticPageDataTemplate.Json);
    var webTemplate = await DownloadWebAsync(page.StaticPageWebsiteTemplate.Url);

    var templateContext = new TemplateContext();
    var scriptObject = new ScriptObject { { "data", dic },{ "products", products } };
    templateContext.PushGlobal(scriptObject);
    Template scribanTemplate = Template.Parse(webTemplate);
    string result = scribanTemplate.Render(templateContext);

    cache.Set<string>($"page_etag_{subdomain}", page.Etag!, TimeSpan.FromMinutes(30));
    cache.Set<string>($"page_{subdomain}", result);

    httpContext.Response.ContentType = "text/html";
    httpContext.Response.Headers.ETag = page.Etag;
    await httpContext.Response.WriteAsync(result);
    return;
});


app.Run();

static async Task<Dictionary<string, object>?> DownloadJsonAsync(string url)
{
    using (HttpClient httpClient = new HttpClient())
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string value = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<JObject>(value);
            return jsonObject?.ToObject<Dictionary<string, object>>(); ;
        }
        else
        {
            throw new Exception($"Error al descargar el JSON. Código de estado: {response.StatusCode}");
        }
    }
}

static async Task<string> DownloadWebAsync(string url)
{
    using (HttpClient httpClient = new HttpClient())
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception($"Error al descargar la web template. Código de estado: {response.StatusCode}");
        }
    }
}

static async Task<byte[]> DownloadFavIconAsync(string url)
{
    using (HttpClient httpClient = new HttpClient())
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
        else
        {
            throw new Exception($"Error al descargar el FavIcon. Código de estado: {response.StatusCode}");
        }
    }
}