using AzureConnectedServices.Models.Client;
using AzureConnectedServices.Services.Proto;
using AzureConnectedServices.WebApi.Services.Interfaces;
using MapsterMapper;

namespace AzureConnectedServices.WebApi.Services
{
    public class ClimateDataService : IClimateDataService
    {
        private readonly NoaaWeather.NoaaWeatherClient _client;

        private readonly IMapper _mapper;

        public ClimateDataService(
            NoaaWeather.NoaaWeatherClient client,
            IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ClimateDataResult> GetClimateData(AzureConnectedServices.Models.Client.NoaaClimateDataRequest request)
        {
            var requestMapped = _mapper.Map<AzureConnectedServices.Services.Proto.NoaaClimateDataRequest>(request);
            var response = await _client.NoaaWeatherOperationAsync(requestMapped);

            var count = response.Result.Count();
            AzureConnectedServices.Models.Client.Result[] clientResult = new AzureConnectedServices.Models.Client.Result[count];

            foreach (var item in response.Result.Select((value, i) => new { i, value }))
            {
                var value = item.value;
                var index = item.i;

                clientResult[index] = new AzureConnectedServices.Models.Client.Result
                {
                    Station = item.value.Station,
                    Value = item.value.Value,
                    Attributes = item.value.Attributes,
                    Datatype = item.value.Datatype,
                    Date = item.value.Date.ToDateTime(),
                };
            }

            ClimateDataResult result = new ClimateDataResult
            {
                Metadata = _mapper.Map<AzureConnectedServices.Models.Client.Metadata>(response.Metadata),
                Results = clientResult,
            };

            return result;
        }
    }
}
