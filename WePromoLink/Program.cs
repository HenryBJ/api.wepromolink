using System.Net.Http.Headers;
using System.Text;
using BTCPayServer.Client;
using FirebaseAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using WePromoLink;
using WePromoLink.Data;
using WePromoLink.Services;
using WePromoLink.Settings;
using WePromoLink.Validators;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Stripe;
using Google.Apis.Auth.OAuth2;
using WePromoLink.Services.Email;
using Azure.Storage.Blobs;
using WePromoLink.Shared.RabbitMQ;

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
builder.Services.AddScoped<IPStackService>(_ =>
{
    return new IPStackService(builder.Configuration["IpStack:ApiKey"]);
});
builder.Services.AddScoped<CampaignValidator>();
builder.Services.AddScoped<FundSponsoredLinkValidator>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddTransient<BTCPaymentService>();
builder.Services.AddScoped<BTCPayServerClient>(x =>
{
    var s = x.GetRequiredService<IOptions<BTCPaySettings>>();

    HttpClient c = new HttpClient();
    var cad = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{s.Value.Email}:{s.Value.Password}"));
    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cad);
    var cc = new BTCPayServerClient(new Uri(s.Value.Url), c);
    return cc;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ILinkService, LinkService>();
builder.Services.AddSingleton<MessageBroker<Hit>>(sp =>
{
    return new MessageBroker<Hit>(new MessageBrokerOptions
    {
        HostName = builder.Configuration["RabbitMQ:hostname"],
        UserName = builder.Configuration["RabbitMQ:username"],
        Password = builder.Configuration["RabbitMQ:password"]
    });
});

builder.Services.AddSingleton<MessageBroker<Event>>(sp =>
{
    return new MessageBroker<Event>(new MessageBrokerOptions
    {
        HostName = builder.Configuration["RabbitMQ:hostname"],
        UserName = builder.Configuration["RabbitMQ:username"],
        Password = builder.Configuration["RabbitMQ:password"]
    });
});
builder.Services.AddTransient<ICampaignService, CampaignService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<BlobServiceClient>(_ =>
{
    return new BlobServiceClient(builder.Configuration["Azure:blob:connectionstring"]);
});
builder.Services.AddTransient<IEmailSender, EmailSender>(_ =>
{
    return new EmailSender(
        builder.Configuration["Email:Server"],
        Convert.ToInt32(builder.Configuration["Email:Port"]),
        builder.Configuration["Email:Sender"],
        builder.Configuration["Email:Password"]);
});
builder.Services.AddScoped<IDataService, DataService>();
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

app.MapGet("/", () => $"WePromoLink API v1.0.3 - {DateTime.Now.ToShortDateString()}");


// Access to link (HIT)
app.MapGet("/{link}", async (string link, HttpContext ctx, ILinkService service) =>
{
    if (String.IsNullOrEmpty(link)) return Results.BadRequest();
    var url = await service.HitLink(new Hit
    {
        LinkId = link,
        Origin = ctx.Request.HttpContext.Connection.RemoteIpAddress.ToString(),
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
