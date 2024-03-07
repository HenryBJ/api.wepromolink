using Microsoft.EntityFrameworkCore;
using WePromoLink;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.HitWorker;
using WePromoLink.Services;
using WePromoLink.Services.Cache;
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

        services.AddSingleton<IShareCache>(x =>
        {
            return new RedisCache(
                configuration["Redis:Host"],
                configuration["Redis:Port"],
                configuration["Redis:Password"]);
        });
        services.AddHttpContextAccessor();

        services.AddTransient<IPushService, PushService>();
   
        services.AddSingleton<MessageBroker<Hit>>(_ =>
        {
            return new MessageBroker<Hit>(new MessageBrokerOptions
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

        services.AddSingleton<MessageBroker<StatsBaseCommand>>(sp =>
        {
            return new MessageBroker<StatsBaseCommand>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<MessageBroker<GeoLocalizeHitCommand>>(sp =>
        {
            return new MessageBroker<GeoLocalizeHitCommand>(new MessageBrokerOptions
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
