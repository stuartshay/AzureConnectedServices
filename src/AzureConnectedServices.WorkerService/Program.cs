using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.WorkerService;
using AzureConnectedServices.WorkerService.Extensions;
using TinyHealthCheck;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostingContext, services) =>
    {
        var settings = hostingContext?.Configuration?.GetSection("Settings");
        services.Configure<Settings>(settings);

        services.AddSingleton<WorkerStateService>();
        services.AddHostedService<Worker>();
        services.AddBasicTinyHealthCheckWithUptime(config =>
        {
            config.Port = 3901;
            config.Hostname = "*";
            return config;
        });
        services.AddCustomTinyHealthCheck<CustomHealthCheck>(config =>
        {
            config.Port = 3902;
            config.Hostname = "*";
            return config;
        });
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var configuration = config.Build();
        var azAppConfigConnection = configuration["AppConfig"] != null ?
              configuration["AppConfig"] : Environment.GetEnvironmentVariable("ENDPOINTS_APPCONFIG");

        config.AddAzureAppConfiguration(options =>
        {
             options.Connect(azAppConfigConnection)
              .ConfigureRefresh(refresh =>
              {
                  refresh.Register("AzureConnectedServices:Settings", refreshAll: true);
              });
        });

        //https://www.johanohlin.com/posts/2019-10-22-using-azure-app-config-in-a-worker-service/
        //https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-dotnet-core-app
    })
    .Build();

await host.RunAsync();
