using AzureConnectedServices.Models.Client;
using Google.Protobuf.WellKnownTypes;
using Mapster;

namespace AzureConnectedServices.WebApi.Mappings
{
    public class NoaaClimateDataMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            SetupProtoToServiceModel(config);
            SetupServiceModelsToProto(config);
        }

        private static void SetupProtoToServiceModel(TypeAdapterConfig config)
        {
            config.NewConfig<AzureConnectedServices.Services.Proto.Result, Result>()
                .Map(d => d.Date, s => s.Date.ToDateTime());
        }

        private static void SetupServiceModelsToProto(TypeAdapterConfig config)
        {
            config.NewConfig<NoaaClimateDataRequest, AzureConnectedServices.Services.Proto.NoaaClimateDataRequest>();
        }
    }
}
