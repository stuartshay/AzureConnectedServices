using AzureConnectedServices.GrpcService.Models;
using AzureConnectedServices.Models.Client;
using ProtoBuf.Grpc;
using AzureConnectedServices.Core.HttpClients;

namespace AzureConnectedServices.GrpcService.Services.CodeFirst
{
    /// <summary>
    /// 
    /// </summary>
    public class NoaaWeatherService : INoaaWeatherService
    {
        private readonly INoaaClimateDataClient _noaaWeatherClient;

        private readonly ILogger<NoaaWeatherService> _logger;

        private readonly HttpClient _httpClient;

        public NoaaWeatherService(INoaaClimateDataClient noaaWeatherClient, ILogger<NoaaWeatherService> logger) 
        {
            _noaaWeatherClient = noaaWeatherClient;
        }


        public Task<SampleCodeFirstReply> UnaryOperation(SampleCodeFirstRequest request, CallContext context = default)
        {
            var content = $"Your request content was '{request.Content}'|{DateTime.Now}";
            return Task.FromResult(new SampleCodeFirstReply { Content = content });
        }

        public async Task<ClimateDataResult> WeatherOperation(NoaaClimateDataRequest request, CallContext context = default)
        {
            var content = $"Your request content was '{request.StationId}'|{DateTime.Now}";

            var requestHardCode = new NoaaClimateDataRequest
            {
                StationId = "GHCND:USW00094728",
                DataSetId = "GHCND",
                StartDate = "2001-01-01",
                EndDate = "2001-01-01",
                Limit = 100,
            };

            ClimateDataResult? result = await _noaaWeatherClient.ClimateData(requestHardCode);
            return result;
        }
    }
}
