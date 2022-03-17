using Azure.Identity;
using AzureConnectedServices.Services;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

//SetupConfiguration();
SetupServices();
AddServices();

var app = builder.Build();
SetupApp();
app.Run();

void SetupConfiguration()
{
    // Add Azure App Configuration to the container.
    var azAppConfigConnection = configuration["AppConfig"];
    if (!string.IsNullOrEmpty(azAppConfigConnection))
    {
        // Use the connection string if it is available.
        configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(azAppConfigConnection)
                .ConfigureRefresh(refresh =>
                {
                    // All configuration values will be refreshed if the sentinel key changes.
                    refresh.Register("TestApp:Settings:Sentinel", refreshAll: true);
                });
        });
    }
    else if (Uri.TryCreate(configuration["Endpoints:AppConfig"], UriKind.Absolute, out var endpoint))
    {
        // Use Azure Active Directory authentication.
        // The identity of this app should be assigned 'App Configuration Data Reader' or 'App Configuration Data Owner' role in App Configuration.
        // For more information, please visit https://aka.ms/vs/azure-app-configuration/concept-enable-rbac
        configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(endpoint, new DefaultAzureCredential())
                .ConfigureRefresh(refresh =>
                {
                    // All configuration values will be refreshed if the sentinel key changes.
                    refresh.Register("TestApp:Settings:Sentinel", refreshAll: true);
                });
        });
    }

    builder.Services.AddAzureAppConfiguration();
}

void SetupServices()
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void AddServices()
{
    services.AddTransient<IWeatherForecastService, WeatherForecastService>();
}

void SetupApp()
{
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //}
    
    //app.UseAzureAppConfiguration();

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);

}

