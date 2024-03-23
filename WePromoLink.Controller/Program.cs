using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using WePromoLink.Controller;
using WePromoLink.Controller.Tasks;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.Services;
using WePromoLink.Services.Cache;
using WePromoLink.Services.CRM;
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

        var CRMconnectionString = configuration.GetConnectionString("CRM");
        services.AddDbContext<CRMDataContext>(x => x.UseSqlServer(CRMconnectionString));

        services.AddHttpContextAccessor();
        services.AddSingleton<IShareCache>(x =>
        {
            return new RedisCache(
                configuration["Redis:Host"],
                configuration["Redis:Port"],
                configuration["Redis:Password"]);
        });

        services.AddTransient<IPushService, PushService>();
        services.AddTransient<ILeadService, LeadService>();
        services.AddTransient<ICampaignRunnerService, CampaignRunnerService>();

        services.AddTransient<BlobServiceClient>(_ =>
        {
            return new BlobServiceClient(configuration["Azure:blob:connectionstring"]);
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

        services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
        {
            return new MessageBroker<BaseEvent>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });

        services.AddSingleton<CleanImagesTask>();
        services.AddSingleton<CampaignEmailRunnerTask>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
