using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.WorkerService;
using TinyHealthCheck;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var settings = hostContext.Configuration.GetSection("Settings");
        services.Configure<Settings>(settings);

        services.AddHostedService<Worker>();
        services.AddBasicTinyHealthCheckWithUptime(config =>
        {
            config.Port = 3901;
            config.Hostname = "*";
            return config;
        });

    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        //var settings = config.Build();
        //var azAppConfigConnection = settings["AppConfig"] != null ?
        //settings["AppConfig"] : Environment.GetEnvironmentVariable("ENDPOINTS_APPCONFIG");

        //config.AddAzureAppConfiguration(options =>
        //options.Connect(azAppConfigConnection)
        //   .ConfigureRefresh(refresh =>
        //   {
        //       refresh.Register("AzureConnectedServices:Settings", refreshAll: true);
        //   }));
    })
    .Build();

await host.RunAsync();
