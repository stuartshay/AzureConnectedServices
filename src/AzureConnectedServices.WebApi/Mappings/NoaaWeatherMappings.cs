using AzureConnectedServices.Models.Client;
using Mapster;

namespace AzureConnectedServices.WebApi.Mappings
{
    public class NoaaWeatherMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            SetupServiceModelsToProto(config);
        }

        private static void SetupServiceModelsToProto(TypeAdapterConfig config)
        {
            config.NewConfig<NoaaWeatherRequest, Services.Proto.NoaaWeatherRequest>()
               // .Map(d => d.Amount, s => Banking.Proto.Transfer.CustomTypes.DecimalValue.FromDecimal(s.Amount))
               ;
        }
    }
}
