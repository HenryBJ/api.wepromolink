using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.HitWorker;
using WePromoLink.Services;

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
        services.AddScoped<IPStackService>(_ =>
        {
            return new IPStackService(configuration["IpStack:ApiKey"]);
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
