using Azure.Identity;
using AzureConnectedServices.Services;
using AzureConnectedServices.Services.Interfaces;

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
    var azAppConfigConnection = builder.Configuration["AppConfig"];
    if (!string.IsNullOrEmpty(azAppConfigConnection))
    {
        // Use the connection string if it is available.
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(azAppConfigConnection)
                .ConfigureRefresh(refresh =>
                {
                    // All configuration values will be refreshed if the sentinel key changes.
                    refresh.Register("TestApp:Settings:Sentinel", refreshAll: true);
                });
        });
    }
    else if (Uri.TryCreate(builder.Configuration["Endpoints:AppConfig"], UriKind.Absolute, out var endpoint))
    {
        // Use Azure Active Directory authentication.
        // The identity of this app should be assigned 'App Configuration Data Reader' or 'App Configuration Data Owner' role in App Configuration.
        // For more information, please visit https://aka.ms/vs/azure-app-configuration/concept-enable-rbac
        builder.Configuration.AddAzureAppConfiguration(options =>
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
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

void AddServices()
{
    services.AddTransient<IWeatherForecastService, WeatherForecastService>();
}

void SetupApp()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    //app.UseAzureAppConfiguration();

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

