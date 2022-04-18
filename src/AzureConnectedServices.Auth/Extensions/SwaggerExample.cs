using AzureConnectedServices.Auth.Models;
using Swashbuckle.AspNetCore.Filters;

namespace AzureConnectedServices.Auth.Extensions
{
    public class SwaggerExample : IExamplesProvider<AuthenticationRequestBody>
    {
        public AuthenticationRequestBody GetExamples()
        {
            return new AuthenticationRequestBody()
            {
                UserName = "myusername",
                Password = "p@ssw0rd"
            };
        }
    }
}
