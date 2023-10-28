using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Services.Marketing;
using WePromoLink.Services.SubscriptionPlan;
using WePromoLink.Shared.RabbitMQ;

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
            .AddHttpContextAccessor()
            .AddSingleton<ILogger<IEmailSender>>(loggerFactory.CreateLogger<IEmailSender>())
            .AddSingleton<ILogger<ISubPlanService>>(loggerFactory.CreateLogger<ISubPlanService>())
            .AddSingleton<ILogger<StripeService>>(loggerFactory.CreateLogger<StripeService>())
            .AddSingleton<ILogger<StripeService>>(loggerFactory.CreateLogger<StripeService>())
            .AddSingleton<ILogger<UserService>>(loggerFactory.CreateLogger<UserService>())
            .AddSingleton<MessageBroker<BaseEvent>>(sp =>
                {
                    return new MessageBroker<BaseEvent>(new MessageBrokerOptions
                    {
                        HostName = _config["RabbitMQ:hostname"],
                        UserName = _config["RabbitMQ:username"],
                        Password = _config["RabbitMQ:password"]
                    });
                })
            .AddScoped<IMarketingService, MarketingService>()
            .AddScoped<ISubPlanService, SubPlanService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<StripeService>()
            .AddScoped<IEmailSender, EmailSender>()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
           .UseSqlServer(_config["ConnectionStrings:Default"]!)
           .Options;
        _db = new DataContext(options);
    }
}