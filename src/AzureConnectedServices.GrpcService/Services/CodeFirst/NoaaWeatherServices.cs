using AzureConnectedServices.GrpcService.Models;
using AzureConnectedServices.Models.Client;
using ProtoBuf.Grpc;
using AzureConnectedServices.Core.HttpClients;

namespace AzureConnectedServices.GrpcService.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class NoaaWeatherService : INoaaWeatherService
    {
        private readonly INoaaWeatherClient _noaaWeatherClient;

        private readonly ILogger<NoaaWeatherService> _logger;

        private readonly HttpClient _httpClient;

        public NoaaWeatherService(INoaaWeatherClient noaaWeatherClient, ILogger<NoaaWeatherService> logger) 
        {
            _noaaWeatherClient = noaaWeatherClient;
        }


        public Task<SampleCodeFirstReply> UnaryOperation(SampleCodeFirstRequest request, CallContext context = default)
        {
            var content = $"Your request content was '{request.Content}'|{DateTime.Now}";
            return Task.FromResult(new SampleCodeFirstReply { Content = content });
        }

        public async Task<WeatherResult> WeatherOperation(NoaaWeatherRequest request, CallContext context = default)
        {
            var content = $"Your request content was '{request.StationId}'|{DateTime.Now}";

            var requestHardCode = new NoaaWeatherRequest
            {
                StationId = "GHCND:USW00094728",
                DataSetId = "GHCND",
                StartDate = "2001-01-01",
                EndDate = "2001-01-01",
                Limit = 100,
            };

            WeatherResult? result = await _noaaWeatherClient.WeatherForecast(requestHardCode);
            return result;
        }
    }
}
