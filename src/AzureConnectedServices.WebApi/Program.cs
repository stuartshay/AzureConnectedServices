using Azure.Identity;
using AzureConnectedServices.Services;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

SetupConfiguration();
SetupServices();
AddServices();

Console.WriteLine(configuration["AzureConnectedServices:Settings:WeatherRequestQueueUrl"]);


var app = builder.Build();
SetupApp();
app.Run();

void SetupConfiguration()
{
    var azAppConfigConnection = configuration["AppConfig"];
    if (!string.IsNullOrEmpty(azAppConfigConnection))
    {
        configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(azAppConfigConnection)
                .ConfigureRefresh(refresh =>
                {
                    refresh.Register("AzureConnectedServices:Settings:Message", refreshAll: true);
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

