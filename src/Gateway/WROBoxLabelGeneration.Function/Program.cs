using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WROBoxLabelGeneration.Application;
using WROBoxLabelGeneration.Infrastructure;
using WROBoxLabelGeneration.SharedKernel;
using WROBoxLabelGeneration.SharedKernel.Configurations;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        configurationBuilder
            .AddJsonFile("appsettings.json", optional: false)
            .AddCommandLine(args);
    })
    .ConfigureServices((hostBuilder, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<AppSettings>(hostBuilder.Configuration);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value);

        // SharedKernel
        services.AddSharedKernel();

        // Application
        services.AddApplication();

        // Persistence
        services.AddInfrastructure();

        // Persistence
        services.AddPersistence();
    })
    .Build();

host.Run();