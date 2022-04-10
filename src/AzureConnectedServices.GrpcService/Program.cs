using AzureConnectedServices.GrpcService.Services.CodeFirst;
using GrpcBrowser.Configuration;
using ProtoBuf.Grpc.Server;
using AzureConnectedServices.Services.ProtoFirst;
using AzureConnectedServices.Services.Proto;
using AzureConnectedServices.GrpcService.Services;
using AzureConnectedServices.Core.HttpClients;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

AddServices();

var app = builder.Build();
SetupApp();
app.Run();

void AddServices()
{
    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();
    builder.Services.AddCodeFirstGrpc();
    builder.Services.AddGrpcBrowser();

    var config = TypeAdapterConfig.GlobalSettings;
    builder.Services.AddSingleton(config);
    builder.Services.AddScoped<IMapper, ServiceMapper>();

    //Services 
    builder.Services.AddScoped<INoaaClimateDataService, NoaaClimateDataService>();

    // Http Clients
    builder.Services.AddHttpClient<INoaaClimateDataClient, NoaaClimateDataClient>(client =>
    {
        client.BaseAddress = new Uri("https://www.ncdc.noaa.gov");
        client.DefaultRequestHeaders.Add("token", "cbhbnwSDElzXjbovAErPxLGDAGiVQaEb");
    });

}

void SetupApp()
{
    app.UseRouting();

    app.UseGrpcBrowser();
    app.MapGrpcBrowser();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<CodeFirstGreeterService>().AddToGrpcBrowserWithService<ICodeFirstGreeterService>();
        endpoints.MapGrpcService<NoaaWeatherService>().AddToGrpcBrowserWithService<INoaaWeatherService>();

        endpoints.MapGrpcService<NoaaClimateDataServiceGrpc>().AddToGrpcBrowserWithClient <NoaaWeather.NoaaWeatherClient>();
        endpoints.MapGrpcService<ProtoFirstSampleService>().AddToGrpcBrowserWithClient<ProtoFirstGreeter.ProtoFirstGreeterClient>();

        endpoints.MapGrpcReflectionService();
    });

    app.MapGet("/", context =>
    {
        context.Response.StatusCode = 302;
        context.Response.Headers.Add("Location", "https://localhost:7267/grpc");
        return Task.CompletedTask;
    });

}
