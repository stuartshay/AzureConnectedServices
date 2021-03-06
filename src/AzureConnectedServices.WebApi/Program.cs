using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Services;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using AzureConnectedServices.Core.HealthChecks;
using Microsoft.Extensions.Azure;
using System.Reflection;

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
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        c.IncludeXmlComments(xmlFilePath);
    });
}

void AddServices()
{
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
