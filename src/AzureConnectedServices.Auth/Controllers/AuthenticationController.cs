using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AzureConnectedServices.Auth.Models;
using AzureConnectedServices.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AzureConnectedServices.Auth.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        private readonly IUserValidationService _validationService;

        private readonly IConfiguration _configuration;

        private readonly string _secretForKey;
        
        public AuthenticationController(
            IUserValidationService validationService,
            ILogger<AuthenticationController> logger,
            IConfiguration configuration)
        {
            _secretForKey = configuration["Authentication:SecretForKey"];
            _validationService = validationService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationRequestBody authenticationRequestBody)
        {
            // Step 1: Validate the username/password
            var user = await _validationService.ValidateUserCredentials(
                authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (string.IsNullOrEmpty(user.UserName))
            {
                return Unauthorized();
            }


            // Step 2: create a token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretForKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

    }
}
