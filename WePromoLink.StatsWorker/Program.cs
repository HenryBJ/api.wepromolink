using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands.Statistics;
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
        services.AddHttpContextAccessor();
        services.AddTransient<IPushService, PushService>();
        services.AddSingleton<IMongoClient>(new MongoClient(configuration["Mongodb:ConnectionString"]));

        services.AddSingleton<MessageBroker<AddClickCommand>>(_ =>
        {
            return new MessageBroker<AddClickCommand>(new MessageBrokerOptions
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
