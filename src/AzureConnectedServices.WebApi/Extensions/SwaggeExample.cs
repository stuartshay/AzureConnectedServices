using AzureConnectedServices.Models;
using Swashbuckle.AspNetCore.Filters;

namespace AzureConnectedServices.WebApi.Extensions
{
    public class SwaggeExample : IExamplesProvider<WeatherRequestModel>
    {
        public WeatherRequestModel GetExamples()
        {
            return new WeatherRequestModel()
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
