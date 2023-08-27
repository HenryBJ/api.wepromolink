using WePromoLink.BtcpayWorker;
using WePromoLink.DTO.Events;
using WePromoLink.Shared.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
        // {
        //     return new MessageBroker<BaseEvent>(new MessageBrokerOptions
        //     {
        //         HostName = configuration["RabbitMQ:hostname"],
        //         UserName = configuration["RabbitMQ:username"],
        //         Password = configuration["RabbitMQ:password"]
        //     });
        // });
        
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
