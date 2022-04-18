using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureConnectedServices.Auth.Controllers;

[Route("api/profile")]
[ApiController]
public class ProfileController : ControllerBase
{
    private ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    [HttpGet("userprofile")]
    [Authorize]
    public IActionResult UserProfile()
    {
        var list = new List<Tuple<string, string,string>>();
            
        foreach (var claim in User.Claims)
        {
            list.Add(new Tuple<string, string, string>(claim.Type, claim.Value,""));
        }

        return Ok(list);
    }

    [HttpGet("public")]
    public IActionResult PublicAPI()
    {
        var list = new[]
        {
            new { Code = 1, Name = "This end point can be accessed by Public" },
            new { Code = 2, Name = "Whatever" }
        }.ToList();

        return Ok(list);
    }
}
