using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Services.Proto;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureConnectedServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastGrpcController : ControllerBase
    {
        private readonly NoaaWeather.NoaaWeatherClient _client;

        private readonly ILogger<WeatherForecastGrpcController> _logger;

        private readonly IMapper _mapper;

        private readonly Settings _settings;

        public WeatherForecastGrpcController(NoaaWeather.NoaaWeatherClient client,
            IMapper mapper,
            ILogger<WeatherForecastGrpcController> logger, 
            IOptionsSnapshot<Settings> settings)
        {
            _logger = logger;
            _mapper = mapper;
            _settings = settings.Value;
            _client = client;
        }

        /// <summary>
        /// Post Weather Request to Grpc Service. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("grpc")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PostGprc([FromBody] AzureConnectedServices.Models.Client.NoaaClimateDataRequest request)
        {
            var requestMapped = _mapper.Map<NoaaClimateDataRequest>(request);

            var response = await _client.NoaaWeatherOperationAsync(requestMapped);
            //TODO: Map Grpc DateTime to .NET DateTime
            foreach (var item in response.Result)
            {
                var x1 = item.Date;
                //var x = _mapper.Map<Models.Client.Result>(item);
            }

            return Ok(response);
        }
    }
}
