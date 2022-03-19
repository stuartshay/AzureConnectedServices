using System.Net;
using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Models;
using AzureConnectedServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureConnectedServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly Settings _settings;
        
        public WeatherForecastController(IWeatherForecastService weatherForecastService, 
            ILogger<WeatherForecastController> logger, IOptionsSnapshot<Settings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _weatherForecastService.GetWeatherForecast();

            _logger.LogInformation(_settings.WeatherRequestQueueUrl);

            return result;
        }
    }
}
