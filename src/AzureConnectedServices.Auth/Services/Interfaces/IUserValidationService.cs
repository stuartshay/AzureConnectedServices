using AzureConnectedServices.Auth.Models;

namespace AzureConnectedServices.Auth.Services.Interfaces
{
    public interface IUserValidationService
    {
        Task<CityInfoUser> ValidateUserCredentials(string? userName, string? password);
    }
}
