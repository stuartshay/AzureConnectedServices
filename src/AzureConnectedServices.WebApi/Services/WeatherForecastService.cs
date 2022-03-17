using AzureConnectedServices.Models;
using AzureConnectedServices.Services.Interfaces;

namespace AzureConnectedServices.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;

        public static readonly string[] Summaries = new[]
            {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};


        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = WeatherForecastService.Summaries[Random.Shared.Next(WeatherForecastService.Summaries.Length)]
                })
                .ToArray();
        }
    }
}
