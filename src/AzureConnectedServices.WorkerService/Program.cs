using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.WorkerService;
using TinyHealthCheck;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        //var settings = hostContext.Configuration.GetSection("AppConfig");
        //services.Configure<Settings>(settings);

        services.AddHostedService<Worker>();
        //https://blog.bruceleeharrison.com/2021/06/24/monitor-headless-worker-services-in-net-core-5-0/
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

        //https://www.johanohlin.com/posts/2019-10-22-using-azure-app-config-in-a-worker-service/
        //Console.WriteLine($"azAppConfigConnection:{azAppConfigConnection}");


        //config.AddAzureAppConfiguration(options =>
        //options.Connect(azAppConfigConnection)
        //   .ConfigureRefresh(refresh =>
        //   {
        //       refresh.Register("AzureConnectedServices:Settings", refreshAll: true);
        //   }));
    })
    .Build();

await host.RunAsync();
