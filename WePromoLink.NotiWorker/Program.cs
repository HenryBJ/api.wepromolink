using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.NotiWorker;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Services.Cache;
using WePromoLink.Services.SignalR;
using WePromoLink.DTO.SignalR;
using WePromoLink.DTO.Events.Commands.Statistics;

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
        services.AddHttpContextAccessor();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        services.AddSingleton<IShareCache>(x =>
        {
            return new RedisCache(
                configuration["Redis:Host"],
                configuration["Redis:Port"],
                configuration["Redis:Password"]);
        });

        services.AddTransient<IPushService, PushService>();

        services.AddSingleton<MessageBroker<BaseEvent>>(_ =>
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


        services.AddSingleton<MessageBroker<DashboardStatus>>(sp =>
        {
            return new MessageBroker<DashboardStatus>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });
        
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
