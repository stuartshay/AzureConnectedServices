using System.Text.Json;
using AzureConnectedServices.Models.Client;

namespace AzureConnectedServices.WorkerService.Clients
{
    public class NoaaWeatherClient : INoaaWeatherClient
    {
        private readonly HttpClient _client;

        private readonly ILogger<NoaaWeatherClient> _logger;

        public NoaaWeatherClient(HttpClient client, ILogger<NoaaWeatherClient> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger;
        }

        public async Task<WeatherResult> WeatherForecast(NoaaWeatherRequest query)
        {
            var result = new WeatherResult();
            var queryString = $"/cdo-web/api/v2/data?stationid={query.StationId}&datasetid={query.DataSetId}&startdate={query.StartDate}&enddate={query.EndDate}&limit={query.Limit}";

            var response = await _client.GetAsync(queryString);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                result = JsonSerializer.Deserialize<WeatherResult>(jsonResult);   
            }

            return result;
        }
    }
}
