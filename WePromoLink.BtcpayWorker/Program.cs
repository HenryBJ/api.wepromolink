using WePromoLink.BtcpayWorker;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Cache;
using WePromoLink.Shared.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        // services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
        // {
        //     return new MessageBroker<BaseEvent>(new MessageBrokerOptions
        //     {
        //         HostName = configuration["RabbitMQ:hostname"],
        //         UserName = configuration["RabbitMQ:username"],
        //         Password = configuration["RabbitMQ:password"]
        //     });
        // });

        services.AddSingleton<IShareCache>(x =>
        {
            return new RedisCache(
                configuration["Redis:Host"],
                configuration["Redis:Port"],
                configuration["Redis:Password"]);
        });

        services.AddTransient<IPushService, PushService>();
        
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
