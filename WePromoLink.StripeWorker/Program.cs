using Microsoft.EntityFrameworkCore;
using Stripe;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Cache;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.StripeWorker;

IHost host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];
        services.AddHostedService<Worker>();
        services.AddHostedService<CommissionWorker>();
        services.AddHttpContextAccessor();
        services.AddTransient<StripeService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IEmailSender, EmailSender>();

        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

        services.AddSingleton<IShareCache>(x =>
        {
            return new RedisCache(
                configuration["Redis:Host"],
                configuration["Redis:Port"],
                configuration["Redis:Password"]);
        });

        services.AddTransient<IPushService, PushService>();

        services.AddSingleton<MessageBroker<Event>>(_ =>
        {
            return new MessageBroker<Event>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
        {
            return new MessageBroker<BaseEvent>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });
    })
    .Build();

await host.RunAsync();
