using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.Services.Email;
using WePromoLink.Services.Marketing;

namespace WePromoLink.Test;

public abstract class BaseTest
{
    protected readonly DataContext _db;
    protected readonly IConfiguration _config;
    protected readonly ServiceProvider? _serviceProvider;

    public BaseTest()
    {
        _config = new ConfigurationBuilder().AddUserSecrets<BaseTest>().Build();

        var loggerFactory = LoggerFactory.Create(builder =>
       {
           builder.AddConsole(); // Puedes usar otros proveedores de registro aquí
       });

        _serviceProvider = new ServiceCollection()
            .AddSingleton(_config)
            .AddDbContext<DataContext>(x => x.UseSqlServer(_config["ConnectionStrings:Default"]!))
            .AddSingleton<ILogger<IEmailSender>>(loggerFactory.CreateLogger<IEmailSender>())
            .AddScoped<IMarketingService, MarketingService>()
            .AddScoped<IEmailSender, EmailSender>()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
           .UseSqlServer(_config["ConnectionStrings:Default"]!)
           .Options;
        _db = new DataContext(options);
    }
}