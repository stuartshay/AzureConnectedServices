using AzureConnectedServices.Core.HttpClients;
using AzureConnectedServices.Models.Client;
using MapsterMapper;

namespace AzureConnectedServices.GrpcService.Services
{
    public class NoaaClimateDataService : INoaaClimateDataService
    {
        private readonly INoaaClimateDataClient _noaaWeatherClient;

        private readonly IMapper _mapper;

        public NoaaClimateDataService(INoaaClimateDataClient noaaWeatherClient, IMapper mapper, ILogger<NoaaClimateDataService> logger)
        {
            _noaaWeatherClient = noaaWeatherClient;
            _mapper = mapper;
        }

        public async Task<ClimateDataResult> GetClimateData(NoaaClimateDataRequest request)
        {
            var response = await _noaaWeatherClient.ClimateData(request);
            return response;
        }

    }
}
