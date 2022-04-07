using AzureConnectedServices.Services.Proto;
using Microsoft.AspNetCore.Mvc;

namespace AzureConnectedServices.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProtoFirstSampleController : ControllerBase
    {
        private readonly ProtoFirstGreeter.ProtoFirstGreeterClient _protoClient;
        
        private readonly ILogger<ProtoFirstSampleController> _logger;

        public ProtoFirstSampleController(ProtoFirstGreeter.ProtoFirstGreeterClient client, ILogger<ProtoFirstSampleController> logger)
        {
            _logger = logger;
            _protoClient = client;
        }

        /// <summary>
        ///  Get Unary Operation
        /// </summary>
        /// <returns>message</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<long>> GetUnaryOperation()
        {
            SampleProtoFirstRequest t = new SampleProtoFirstRequest
            {
                Content = $"Hellow World:{DateTime.Now}"
            };

            var response = _protoClient.UnaryOperation(t);
            return Ok(response);
        }
    }
}
