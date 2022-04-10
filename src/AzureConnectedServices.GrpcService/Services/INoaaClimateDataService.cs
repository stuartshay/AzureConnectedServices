using AzureConnectedServices.Models.Client;

namespace AzureConnectedServices.GrpcService.Services
{
    public interface INoaaClimateDataService
    {
        public Task<ClimateDataResult> GetClimateData(NoaaClimateDataRequest request);
    }
}
