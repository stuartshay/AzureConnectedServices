using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.WorkerService;
using AzureConnectedServices.WorkerService.Clients;
using AzureConnectedServices.WorkerService.Extensions;
using Microsoft.Extensions.Configuration;
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

        services.AddOptions();
        services.AddAzureAppConfiguration();

        services.AddSingleton<WorkerStateService>();
        services.AddHttpClient<INoaaWeatherClient, NoaaWeatherClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.ncdc.noaa.gov");
            client.DefaultRequestHeaders.Add("token", "cbhbnwSDElzXjbovAErPxLGDAGiVQaEb");
        });
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
