using System.Net.Http.Headers;
using System.Text;
using BTCPayServer.Client;
using FirebaseAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Polly;
using WePromoLink;
using WePromoLink.Data;
using WePromoLink.Services;
using WePromoLink.Settings;
using WePromoLink.Validators;
using WePromoLink.Workers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Stripe;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);
StripeConfiguration.ApiKey = builder.Configuration["Stripe:ApiKey"];

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(builder.Configuration["FirebaseAdmin:Path"])
});


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddControllers();
builder.Services.Configure<BTCPaySettings>(builder.Configuration.GetSection("BTCPay"));
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 2000;
}); //Remplazar por Redis cuando se use microservicios
builder.Services.AddScoped<CampaignValidator>();
builder.Services.AddScoped<AffiliateLinkValidator>();
builder.Services.AddScoped<FundSponsoredLinkValidator>();
builder.Services.AddSingleton<HitQueue>();
builder.Services.AddSingleton<WebHookEventQueue>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IPaymentService, BTCPaymentService>();
builder.Services.AddScoped<BTCPayServerClient>(x =>
{
    var s = x.GetRequiredService<IOptions<BTCPaySettings>>();

    HttpClient c = new HttpClient();
    var cad = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{s.Value.Email}:{s.Value.Password}"));
    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cad);
    var cc = new BTCPayServerClient(new Uri(s.Value.Url), c);
    return cc;
});
builder.Services.AddHostedService<HitWorker>();
builder.Services.AddHostedService<WebHookWorker>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAffiliateLinkService, AffiliateLinkService>();
builder.Services.AddTransient<ICampaignService, CampaignService>();
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IPricingService, PricingService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<StripeService>();
builder.Services.AddTransient<IStatsLinkService, StatsLinkService>();
builder.Services.AddCors(e => e.AddPolicy("MyCORSPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// firebase auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["Jwt:Firebase:ValidIssuer"];
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Firebase:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:Firebase:ValidAudience"]
    };
});

var app = builder.Build();

app.UseCors("MyCORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

InitializeDataBase(app);

app.MapGet("/", () => "WePromoLink API v1.0.3 - 29/04/2023");


// Access to affiliate links (HIT)
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
// app.MapPost("/webhook", async (HttpContext ctx, IPaymentService service) =>
// {
//     await service.HandleWebHook(ctx);
//     return Results.Ok();
// });

app.MapControllers();
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
