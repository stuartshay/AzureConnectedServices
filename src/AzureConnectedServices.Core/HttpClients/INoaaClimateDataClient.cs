using System.Threading.Tasks;
using AzureConnectedServices.Models.Client;

namespace AzureConnectedServices.Core.HttpClients
{
    public interface INoaaClimateDataClient
    {
        Task<ClimateDataResult> ClimateData(NoaaClimateDataRequest request);
    }
}
