using Microsoft.AspNetCore.SignalR;
using System.Text;
using GoogleAuthenticatorService.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Cache;
using WePromoLink.Services.Email;
using WePromoLink.Services.SignalR;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.DTO.SignalR;
using WePromoLink.Backoffice.Worker;
using WePromoLink.Services.SubscriptionPlan;
using WePromoLink.Services.StaticPages;
using Azure.Storage.Blobs;
using NameCheap;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["SignalR:ConnectionString"]);
builder.Services.AddTransient<BlobServiceClient>(_ =>
{
    return new BlobServiceClient(builder.Configuration["Azure:blob:connectionstring"]);
});
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSingleton<IShareCache>(x =>
{
    return new RedisCache(
        builder.Configuration["Redis:Host"],
        builder.Configuration["Redis:Port"],
        builder.Configuration["Redis:Password"]);
});

builder.Services.AddSingleton<NameCheapApi>(x =>
{
    return new NameCheapApi(
        builder.Configuration["NameCheap:ApiUser"],
        builder.Configuration["NameCheap:ApiUser"],
        builder.Configuration["NameCheap:ApiKey"],
        builder.Configuration["NameCheap:ClientIP"]);
});

builder.Services.AddSingleton<AdminDashboardService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ISubPlanService, SubPlanService>();
builder.Services.AddTransient<IStaticPageService, StaticPageService>();

builder.Services.AddTransient<IPushService, PushService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<StripeService>();

builder.Services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
{
    return new MessageBroker<BaseEvent>(new MessageBrokerOptions
    {
        HostName = builder.Configuration["RabbitMQ:hostname"],
        UserName = builder.Configuration["RabbitMQ:username"],
        Password = builder.Configuration["RabbitMQ:password"]
    });
});

builder.Services.AddSingleton<MessageBroker<DashboardStatus>>(sp =>
{
    return new MessageBroker<DashboardStatus>(new MessageBrokerOptions
    {
        HostName = builder.Configuration["RabbitMQ:hostname"],
        UserName = builder.Configuration["RabbitMQ:username"],
        Password = builder.Configuration["RabbitMQ:password"]
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "wepromolink.com",
        ValidAudience = "wepromolink.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:secret"])),
        ClockSkew = TimeSpan.Zero // Opcional: para permitir una ventana de tiempo pequeña para la expiración del token
    };
});

builder.Services.AddHostedService<DashboardWorker>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
//         {
//             return new MessageBroker<BaseEvent>(new MessageBrokerOptions
//             {
//                 HostName = configuration["RabbitMQ:hostname"],
//                 UserName = configuration["RabbitMQ:username"],
//                 Password = configuration["RabbitMQ:password"]
//             });
//         });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(builder =>
   {
       builder.WithOrigins("https://dashboard.wepromolink.com", "http://localhost:3000")
           .AllowAnyHeader()
           .WithMethods("GET", "POST", "PUT", "DELETE")
           .AllowCredentials();
   });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

Send2kFactorQR(app, builder.Configuration["Auth:user"], builder.Configuration["Auth:secret"]);



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<DashboardHub>("/dashboard");
    endpoints.MapGet("/", () => $"WePromoLink ADMIN API v1.0.3 - {DateTime.Now.ToShortDateString()}");
    endpoints.MapControllers();
});

// app.MapHub<DashboardHub>("/dashboard");
// app.MapGet("/", () => $"WePromoLink ADMIN API v1.0.3 - {DateTime.Now.ToShortDateString()}");
// app.MapControllers();

app.Run();

void Send2kFactorQR(IApplicationBuilder app, string email, string secret)
{
    using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        TwoFactorAuthenticator Authenticator = new TwoFactorAuthenticator();
        var SetupResult = Authenticator.GenerateSetupCode("WePromoLink Admin 2KF", secret, 250, 250);
        string qrUrl = SetupResult.QrCodeSetupImageUrl;
        string manualCode = SetupResult.ManualEntryKey;

        Console.WriteLine("Sending 2KF QR...");
        scope.ServiceProvider?.GetService<IEmailSender>()?
        .Send("Admin", email, "WePromoLink 2KF QR", Templates.GenerateQR(new { ManualCode = manualCode, QrCodeUrl = qrUrl }));
    }
};