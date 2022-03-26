using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.WorkerService;
using AzureConnectedServices.WorkerService.Extensions;
using TinyHealthCheck;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
     {
        var configuration = config.Build();
        var azAppConfigConnection = configuration["AppConfig"] != null ?
           configuration["AppConfig"] : Environment.GetEnvironmentVariable("ENDPOINTS_APPCONFIG");

        config.AddAzureAppConfiguration(options =>
         {
            options.Connect(azAppConfigConnection)
            .Select("AzureConnectedServices:*")
            .ConfigureRefresh(refresh =>
            {
              refresh.Register("AzureConnectedServices:Settings:ServiceBusConnectionString", refreshAll: true);
            });
         });
    })
    .ConfigureServices((hostingContext, services) =>
    {
        var settings = hostingContext?.Configuration?.GetSection("Settings");
        services.Configure<Settings>(settings);
        
        services.AddAzureAppConfiguration();
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
    .Build();

await host.RunAsync();
