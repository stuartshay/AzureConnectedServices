using System.Threading.Tasks;
using AzureConnectedServices.Models.Client;

namespace AzureConnectedServices.WorkerService.Clients
{
    public interface INoaaWeatherClient
    {
        Task<WeatherResult> WeatherForecast(NoaaWeatherRequest request);
    }
}
