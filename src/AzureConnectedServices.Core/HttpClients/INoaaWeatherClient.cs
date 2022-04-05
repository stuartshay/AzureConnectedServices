using System.Threading.Tasks;
using AzureConnectedServices.Models.Client;

namespace AzureConnectedServices.Core.HttpClients
{
    public interface INoaaWeatherClient
    {
        Task<WeatherResult> WeatherForecast(NoaaWeatherRequest request);
    }
}
