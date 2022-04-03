using AzureConnectedServices.GrpcService.Services.CodeFirst;
using GrpcBrowser.Configuration;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

AddServices();

var app = builder.Build();
SetupApp();
app.Run();

void AddServices()
{
   builder.Services.AddGrpc();
   builder.Services.AddCodeFirstGrpc();
   builder.Services.AddGrpcBrowser();
}

void SetupApp()
{
    app.UseRouting();

    app.UseGrpcBrowser();
    app.MapGrpcBrowser();
    app.MapGet("/", context =>
    {
        context.Response.StatusCode = 302;
        context.Response.Headers.Add("Location", "https://localhost:7267/grpc");
        return Task.CompletedTask;
    });

    app.MapGrpcService<CodeFirstGreeterService>().AddToGrpcBrowserWithService<ICodeFirstGreeterService>();
    app.MapGrpcService<NoaaWeatherService>().AddToGrpcBrowserWithService<INoaaWeatherService>();

   // app.MapGrpcService<ProtoFirstSampleService>().AddToGrpcBrowserWithClient<ProtoFirstGreeter.ProtoFirstGreeterClient>();
}
