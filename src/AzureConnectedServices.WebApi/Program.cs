using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Services;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using AzureConnectedServices.Core.HealthChecks;
using Microsoft.Extensions.Azure;
using System.Reflection;
using AzureConnectedServices.Core.Logging;
using AzureConnectedServices.Core.Queue;
using Swashbuckle.AspNetCore.Filters;
using AzureConnectedServices.Services.Proto;
using AzureConnectedServices.WebApi.Mappings;
using AzureConnectedServices.WebApi.Services;
using AzureConnectedServices.WebApi.Services.Interfaces;
using Google.Protobuf.Collections;
using Mapster;
using MapsterMapper;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

SetupConfiguration();
SetupLogger();
AddMappings();
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

void SetupLogger()
{
    builder.Host.UseSerilog(Logging.ConfigureLogger);
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

    services.AddAzureClients(a =>
    {
        a.AddServiceBusClient(configuration["AzureConnectedServices:Settings:ServiceBusConnectionString"]);
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

void AddMappings()
{
    var assemblies = new List<Assembly>();
    var mapping = Assembly.GetAssembly(typeof(NoaaClimateDataMappings));
    if (mapping != null)
    {
        assemblies.Add(mapping);
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        TypeAdapterConfig.GlobalSettings.Scan(assemblies.ToArray());
        TypeAdapterConfig.GlobalSettings.Default.UseDestinationValue(member => member.SetterModifier == AccessModifier.None && member.Type.IsGenericType && member.Type.GetGenericTypeDefinition() == typeof(RepeatedField<>));
    }
}

void AddServices()
{
    var config = TypeAdapterConfig.GlobalSettings;
    services.AddSingleton(config);
    services.AddScoped<IMapper, ServiceMapper>();

    services.AddTransient<IClimateDataService, ClimateDataService>();
    services.AddTransient<IWeatherForecastService, WeatherForecastService>();

    services.AddTransient<IMessageService, AzureServiceBusService>();
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
