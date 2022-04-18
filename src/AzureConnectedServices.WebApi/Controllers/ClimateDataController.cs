using AzureConnectedServices.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureConnectedServices.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class ClimateDataController : ControllerBase
{
    private readonly IClimateDataService _climateDataService;

    private readonly ILogger<ClimateDataController> _logger;

    public ClimateDataController(
        IClimateDataService climateDataService,
        ILogger<ClimateDataController> logger)
    {
        _climateDataService = climateDataService; 
        _logger = logger;
    }

    /// <summary>
    /// Post Climate Data Request to Grpc Service. 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("grpc")]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> PostGrpc([FromBody] AzureConnectedServices.Models.Client.NoaaClimateDataRequest request)
    {
        _logger.LogInformation("Request:{request}", request);
            
        var response = await _climateDataService.GetClimateData(request);

        return Ok(response);
    }
}
