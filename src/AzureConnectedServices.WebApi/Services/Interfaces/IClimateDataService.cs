using AzureConnectedServices.Models.Client;

namespace AzureConnectedServices.WebApi.Services.Interfaces
{
    public interface IClimateDataService
    {
        Task<ClimateDataResult> GetClimateData(NoaaClimateDataRequest request);
    }
}
