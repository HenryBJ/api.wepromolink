using Stripe;
using WePromoLink.Services;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.StripeWorker;

IHost host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostContext, config) =>
    {
        StripeConfiguration.ApiKey = hostContext.Configuration["Stripe:ApiKey"];
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddHostedService<Worker>();
        services.AddTransient<StripeService>();

        services.AddSingleton<MessageBroker<Event>>(_ =>
        {
            return new MessageBroker<Event>(new MessageBrokerOptions
            {
                HostName = configuration["RabbitMQ:hostname"],
                UserName = configuration["RabbitMQ:username"],
                Password = configuration["RabbitMQ:password"]
            });
        });
    })
    .Build();

await host.RunAsync();
