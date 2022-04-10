using System.Text.Json;
using AzureConnectedServices.Models.Client;
using AzureConnectedServices.Services.Proto;
using Microsoft.Extensions.Logging;
using NoaaClimateDataRequest = AzureConnectedServices.Models.Client.NoaaClimateDataRequest;

namespace AzureConnectedServices.Core.HttpClients
{
    public class NoaaClimateDataClient : INoaaClimateDataClient
    {
        private readonly HttpClient _client;

        private readonly ILogger<NoaaWeather.NoaaWeatherClient> _logger;

        public NoaaClimateDataClient(HttpClient client, ILogger<NoaaWeather.NoaaWeatherClient> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger;
        }

        public async Task<ClimateDataResult> ClimateData(NoaaClimateDataRequest query)
        {
            var result = new ClimateDataResult();
            var queryString = $"/cdo-web/api/v2/data?stationid={query.StationId}&datasetid={query.DataSetId}&startdate={query.StartDate}&enddate={query.EndDate}&limit={query.Limit}";

            var response = await _client.GetAsync(queryString);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                result = JsonSerializer.Deserialize<ClimateDataResult>(jsonResult);
            }

            return result;
        }
    }
}
