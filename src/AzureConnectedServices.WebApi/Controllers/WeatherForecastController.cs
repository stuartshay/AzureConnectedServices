using Azure.Messaging.ServiceBus;
using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Models;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureConnectedServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly Settings _settings;

        private readonly ServiceBusClient _client;

        public WeatherForecastController(ServiceBusClient client, IWeatherForecastService weatherForecastService, 
            ILogger<WeatherForecastController> logger, IOptionsSnapshot<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            _weatherForecastService = weatherForecastService;
            _client = client;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseData<IEnumerable<WeatherForecast>>))]
        //[ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(Common.Models.Swagger.Failure))]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            var result = _weatherForecastService.GetWeatherForecast();

            var sender = _client.CreateSender("weatherrequest");
            await sender.SendMessageAsync((new ServiceBusMessage($"Message: {result}")));

            return Ok(result);
        }
    }
}
