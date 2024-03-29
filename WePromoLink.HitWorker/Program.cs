using Microsoft.EntityFrameworkCore;
using WePromoLink;
using WePromoLink.Data;
using WePromoLink.HitWorker;
using WePromoLink.Services;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;

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

        services.AddSingleton<MessageBroker<Hit>>(_ =>
        {
            return new MessageBroker<Hit>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<UpdateCampaignMessage>>(sp =>
        {
            return new MessageBroker<UpdateCampaignMessage>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<UpdateUserMessage>>(sp =>
        {
            return new MessageBroker<UpdateUserMessage>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<UpdateLinkMessage>>(sp =>
        {
            return new MessageBroker<UpdateLinkMessage>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });


        services.AddScoped<IPStackService>(_ =>
        {
            return new IPStackService(configuration["IpStack:ApiKey"]);
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
