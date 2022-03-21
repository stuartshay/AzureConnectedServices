using AzureConnectedServices.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {

    })
    .Build();

await host.RunAsync();
