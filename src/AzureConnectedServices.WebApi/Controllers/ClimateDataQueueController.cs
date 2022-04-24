using Azure.Messaging.ServiceBus;
using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Core.Constants;
using AzureConnectedServices.Core.Queue;
using AzureConnectedServices.Models.Message;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureConnectedServices.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClimateDataQueueController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        private readonly ILogger<ClimateDataQueueController> _logger;

        private readonly Settings _settings;

        private readonly ServiceBusClient _client;

        private readonly IMessageService _messageService;

        public ClimateDataQueueController(
            ServiceBusClient client,
            IWeatherForecastService weatherForecastService,
            IMessageService messageService,
            ILogger<ClimateDataQueueController> logger, 
            IOptionsSnapshot<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            _weatherForecastService = weatherForecastService;
            _messageService = messageService;
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
            //Message Service
           var messageMetadata = new MessageMetadata
           {
               CorrelationId = Guid.NewGuid(), 
               MessageId = Guid.NewGuid(),
           };

           await _messageService.SendAsync(request, SettingsConstant.ClimateDataQueue, messageMetadata);
           return Ok(true);
        }

    }
}
