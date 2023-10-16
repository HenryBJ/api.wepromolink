using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using WePromoLink;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Settings;
using WePromoLink.Validators;
using WePromoLink.Workers;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.Configure<BTCPaySettings>(builder.Configuration.GetSection("BTCPay"));
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<SponsoredLinkValidator>();
builder.Services.AddScoped<AffiliateLinkValidator>();
builder.Services.AddScoped<FundSponsoredLinkValidator>();
builder.Services.AddSingleton<HitQueue>();
builder.Services.AddSingleton<BTCPayServer.Client.BTCPayServerClient>(x =>
{
    var ctx = builder.Services.BuildServiceProvider();
    using (var scope = ctx.CreateScope())
    {
        var s = scope.ServiceProvider.GetRequiredService<IOptions<BTCPaySettings>>();
        return new BTCPayServer.Client.BTCPayServerClient(new Uri(s.Value.Url),s.Value.Email,s.Value.Password);
    }
});
builder.Services.AddHostedService<HitWorker>();
builder.Services.AddTransient<IPaymentService, BTCPaymentService>();
builder.Services.AddTransient<IAffiliateLinkService, AffiliateLinkService>();
builder.Services.AddTransient<ISponsoredLinkService, SponsoredLinkService>();
builder.Services.AddTransient<IStatsLinkService, StatsLinkService>();
builder.Services.AddCors(e => e.AddPolicy("MyCORSPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();

app.UseCors("MyCORSPolicy");

InitializeDataBase(app);

app.MapGet("/", () => "WePromoLink API v1.0");

// List sponsored links
app.MapGet("/links", async (int? page, ISponsoredLinkService service) =>
{
    try
    {
        var results = await service.ListSponsoredLinks(page);
        return Results.Ok(results);
    }
    catch (System.Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem();
    }
});

// Get Stats for affiliate link
app.MapGet("/stats/affiliate/{afflinkId}", async (string afflinkId, IStatsLinkService service) =>
{
    if (String.IsNullOrEmpty(afflinkId)) return Results.BadRequest();
    var result = await service.AffiliateLinkStats(afflinkId);
    return Results.Ok(result);
});


// Get Stats for sponsored link
app.MapGet("/stats/sponsored/{linkId}", async (string linkId, IStatsLinkService service) =>
{
    if (String.IsNullOrEmpty(linkId)) return Results.BadRequest();
    var result = await service.SponsoredLinkStats(linkId);
    return Results.Ok(result);

});

//Fund sponsored link
app.MapPost("/fund", async (FundSponsoredLink funLinkId, FundSponsoredLinkValidator validator, ISponsoredLinkService service) =>
{
    // try
    // {
        if (funLinkId == null) return Results.BadRequest();
        var validationResult = await validator.ValidateAsync(funLinkId);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        var result = await service.FundSponsoredLink(funLinkId);
        return Results.Ok(result);

    // }
    // catch (System.Exception ex)
    // {
    //     Console.WriteLine(ex.Message);
    //     return Results.Problem();
    // }

});

//Create sponsored link
app.MapPost("/link", async (CreateSponsoredLink link, SponsoredLinkValidator validator, ISponsoredLinkService service) =>
{
    if (link == null) return Results.BadRequest();

    var validationResult = await validator.ValidateAsync(link);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    var result = await service.CreateSponsoredLink(link);
    return Results.Ok(result);

});

//Create affiliate link
app.MapPost("/afflink", async (CreateAffiliateLink link, HttpContext ctx, AffiliateLinkValidator validator, IAffiliateLinkService service) =>
{
    if (link == null) return Results.BadRequest();

    var validationResult = await validator.ValidateAsync(link);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    object result;
    try
    {
        result = await service.CreateAffiliateLink(link, ctx);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem(ex.Message);
    }

    return Results.Ok(result);
});

// Access to affiliate links
app.MapGet("/{afflink}", async (string afflink, HttpContext ctx, IAffiliateLinkService service) =>
{
    if (String.IsNullOrEmpty(afflink)) return Results.BadRequest();
    var url = await service.HitAffiliateLink(new HitAffiliate
    {
        AffLinkId = afflink,
        Origin = ctx.Request.HttpContext.Connection.RemoteIpAddress,
        HitAt = DateTime.UtcNow
    });
    return String.IsNullOrEmpty(url) ? Results.NotFound() : Results.Redirect(url);
});

//BTCPay webhook
app.MapPost("/webhook", async (HttpContext ctx, IPaymentService service) =>
{
    await service.HandleWebHook(ctx);
    return Results.Ok();
});

app.Run();



void InitializeDataBase(IApplicationBuilder app)
{
    Policy
    .Handle<Exception>()
    .WaitAndRetry(9, r => TimeSpan.FromSeconds(5))
    .Execute(() =>
    {
        using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            Console.WriteLine("Migration DB...");
            scope.ServiceProvider?.GetService<DataContext>()?.MigrateDB();
        }
    });
};
