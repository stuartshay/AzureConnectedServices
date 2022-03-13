using AzureConnectedServices.Models;

namespace AzureConnectedServices.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}
