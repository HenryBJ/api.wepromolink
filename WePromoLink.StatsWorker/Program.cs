using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Cache;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.StatsWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
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

        services.AddSingleton<MessageBroker<UpdateCampaignMessage>>(_ =>
        {
            return new MessageBroker<UpdateCampaignMessage>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<UpdateUserMessage>>(_ =>
        {
            return new MessageBroker<UpdateUserMessage>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<UpdateLinkMessage>>(_ =>
        {
            return new MessageBroker<UpdateLinkMessage>(new MessageBrokerOptions
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

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
