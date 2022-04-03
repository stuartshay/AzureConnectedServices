using Azure.Messaging.ServiceBus;
using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Models;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

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

        /// <summary>
        /// Post Weather Request to Message Queue. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post([FromBody] WeatherRequestModel request)
        {
            var body = JsonSerializer.Serialize(request);
            //var message = new ServiceBusMessage
            //{
            //    Body = new BinaryData(body),
            //    CorrelationId = System.Guid.NewGuid().ToString(),
            //    MessageId = System.Guid.NewGuid().ToString(),
            //};

            var sender = _client.CreateSender("weatherrequest");
            ServiceBusMessage message = new ServiceBusMessage("body");

            // send the message
            await sender.SendMessageAsync(message);

            return Ok();
        }




    }
}
