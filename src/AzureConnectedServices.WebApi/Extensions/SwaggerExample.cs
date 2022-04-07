using AzureConnectedServices.Models.Client;
using Swashbuckle.AspNetCore.Filters;

namespace AzureConnectedServices.WebApi.Extensions
{
    public class SwaggerExample : IExamplesProvider<AzureConnectedServices.Models.Client.NoaaWeatherRequest>
    {
        public NoaaWeatherRequest GetExamples()
        {
            return new AzureConnectedServices.Models.Client.NoaaWeatherRequest()
            {
                StationId = "GHCND:USW00094728",
                DataSetId = "GHCND",
                StartDate = "2001-01-01",
                EndDate = "2001-01-01",
                Limit = 100,
            };
        }
    }
}
