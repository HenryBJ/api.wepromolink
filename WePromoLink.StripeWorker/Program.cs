using Stripe;
using WePromoLink.Services;
using WePromoLink.StripeWorker;

IHost host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostContext, config) =>
    {
        StripeConfiguration.ApiKey = hostContext.Configuration["Stripe:ApiKey"];
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<StripeService>();
    })
    .Build();

await host.RunAsync();
