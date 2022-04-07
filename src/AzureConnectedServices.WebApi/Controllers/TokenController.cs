using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using AzureConnectedServices.WebApi.Models;

namespace AzureConnectedServices.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public TokenController()
        {

        }

        /// <summary>
        /// Get Jwt Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> GetToken(CredentialModel model)
        {
            //var result = await tokenService.GenerateTokenModelAsync(model);

            //if (result.Success)
            //{
            //    return Results.Created("", new { token = result.Token, expiration = result.Expiration });
            //}

            return Results.BadRequest();
        }
    }
}
