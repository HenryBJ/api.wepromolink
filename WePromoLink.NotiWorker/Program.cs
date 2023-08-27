using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.NotiWorker;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Services.Cache;
using WePromoLink.Services.SignalR;

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

        services.AddSingleton<IAdminDashboardHub>(x =>
        {
            var cache = x.GetRequiredService<IShareCache>();
            return new AdminDashboardHub(configuration["SignalR:ConnectionString"], cache);
        });

        services.AddTransient<IEmailSender, EmailSender>(_ =>
        {
            return new EmailSender(
                configuration["Email:Server"],
                Convert.ToInt32(configuration["Email:Port"]),
                configuration["Email:Sender"],
                configuration["Email:Password"]);
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
