using Azure.Messaging.ServiceBus;
using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AzureConnectedServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastQueueController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        private readonly ILogger<WeatherForecastQueueController> _logger;

        private readonly Settings _settings;

        private readonly ServiceBusClient _client;

        public WeatherForecastQueueController(ServiceBusClient client,IWeatherForecastService weatherForecastService,
            ILogger<WeatherForecastQueueController> logger, 
            IOptionsSnapshot<Settings> settings)
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
        [HttpPost("queue")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PostQueue([FromBody] AzureConnectedServices.Models.Client.NoaaClimateDataRequest request)
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

            return Ok("Hello");
        }

    }
}
