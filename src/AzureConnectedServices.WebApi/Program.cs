using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Services;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using AzureConnectedServices.Core.HealthChecks;
using Microsoft.Extensions.Azure;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using AzureConnectedServices.Services.Proto;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

SetupConfiguration();
SetupServices();
AddServices();

var app = builder.Build();
SetupApp();
app.Run();

void SetupConfiguration()
{
    var azAppConfigConnection = configuration["AppConfig"] != null ? 
        configuration["AppConfig"] : Environment.GetEnvironmentVariable("ENDPOINTS_APPCONFIG");

    if (!string.IsNullOrEmpty(azAppConfigConnection))
    {
        configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(azAppConfigConnection)
                .ConfigureRefresh(refresh =>
                {
                    refresh.Register("AzureConnectedServices:Settings", refreshAll: true);
                });
        });
    }

    builder.Services.AddAzureAppConfiguration();
}

void SetupServices()
{
    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

    services.Configure<Settings>(configuration.GetSection("AzureConnectedServices:Settings"));
    
    services.AddHealthChecks()
        .AddCheck<VersionHealthCheck>("version");

    services
      .AddHealthChecksUI()
      .AddInMemoryStorage();

    services.AddAzureClients(builder =>
    {
        builder.AddServiceBusClient(configuration["AzureConnectedServices:Settings:ServiceBusConnectionString"]);
    });

    services.AddControllers();

    services.AddGrpcClient<ProtoFirstGreeter.ProtoFirstGreeterClient>(c =>
    {
        c.Address = new Uri("https://localhost:7267");
    });
    services.AddGrpcClient<NoaaWeather.NoaaWeatherClient>(c =>
    {
        c.Address = new Uri("https://localhost:7267");
    });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(s =>
    {
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        s.IncludeXmlComments(xmlFilePath);
        s.ExampleFilters();
    });
    services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
}

void AddServices()
{
    var config = TypeAdapterConfig.GlobalSettings;
    services.AddSingleton(config);
    services.AddScoped<IMapper, ServiceMapper>();

    services.AddTransient<IWeatherForecastService, WeatherForecastService>();
}

void SetupApp()
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAzureAppConfiguration();

    app.UseRouting();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("healthz", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
}
