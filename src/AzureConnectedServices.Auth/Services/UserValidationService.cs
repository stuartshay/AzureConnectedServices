using AzureConnectedServices.Auth.Models;
using AzureConnectedServices.Auth.Services.Interfaces;

namespace AzureConnectedServices.Auth.Services
{
    public class UserValidationService : IUserValidationService
    {
        private ILogger<UserValidationService> _logger;

        public UserValidationService(ILogger<UserValidationService> logger)
        {
            _logger = logger;
        }

        public Task<CityInfoUser> ValidateUserCredentials(string? userName, string? password)
        {
            var result = new CityInfoUser(1, userName ?? "", "Michael", "Williams", "New York");

            return Task.FromResult(result);
        }
    }
}
