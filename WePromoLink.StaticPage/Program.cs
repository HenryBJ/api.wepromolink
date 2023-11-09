using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scriban;
using Scriban.Runtime;
using WePromoLink.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<BlobServiceClient>(_ =>
{
    return new BlobServiceClient(builder.Configuration["Azure:blob:connectionstring"]);
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
    httpContext.Response.ContentType = "image/x-icon";
    var subdomain = httpContext.Request.Host.Host;

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
    await httpContext.Response.Body.WriteAsync(faviconData);
});


app.MapGet("/", async (HttpContext httpContext, DataContext db) =>
{
    httpContext.Response.ContentType = "text/html";
    var subdomain = httpContext.Request.Host.Host;
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


    var cacheControl = new CacheControlHeaderValue
    {
        MaxAge = TimeSpan.FromMinutes(30), // Establece el tiempo de caché a 30 minutos
        Public = true
    };
    httpContext.Response.GetTypedHeaders().CacheControl = cacheControl;

    var page = await db.StaticPages
    .Include(e => e.StaticPageDataTemplate)
    .Include(e => e.StaticPageWebsiteTemplate)
    .Where(e => e.Name.ToLower() == subdomain)
    .SingleOrDefaultAsync();

    if (page == null) return httpContext.Response.WriteAsync(defaultResponseHtml);

    var dic = await DownloadJsonAsync(page.StaticPageDataTemplate.Json);
    var webTemplate = await DownloadWebAsync(page.StaticPageWebsiteTemplate.Url);

    var templateContext = new TemplateContext();
    var scriptObject = new ScriptObject { { "data", dic } };
    templateContext.PushGlobal(scriptObject);
    Template scribanTemplate = Template.Parse(webTemplate);
    string result = scribanTemplate.Render(templateContext);

    return httpContext.Response.WriteAsync(result);
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