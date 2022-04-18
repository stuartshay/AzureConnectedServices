using AzureConnectedServices.Models.Client;
using Google.Protobuf.WellKnownTypes;
using Mapster;

namespace AzureConnectedServices.GrpcService.Mappings
{
    public class NoaaClimateDataMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            SetupServiceModelsToProto(config);
            SetupProtoToServiceModel(config);
        }

        private static void SetupServiceModelsToProto(TypeAdapterConfig config)
        {
            config.NewConfig<Result, AzureConnectedServices.Services.Proto.Result>()
                .Map(d => d.Date, s => Timestamp.FromDateTime(DateTime.SpecifyKind(s.Date, DateTimeKind.Utc)));
   
            config.NewConfig<Resultset, AzureConnectedServices.Services.Proto.Resultset>();
        }

        private static void SetupProtoToServiceModel(TypeAdapterConfig config)
        {
            config.NewConfig<NoaaClimateDataRequest, AzureConnectedServices.Services.Proto.NoaaClimateDataRequest>();
            config.NewConfig<AzureConnectedServices.Services.Proto.Resultset, Resultset>();

            config.NewConfig<AzureConnectedServices.Services.Proto.Result, Result>()
                .Map(d => d.Date, s => Timestamp.FromDateTime(DateTime.UtcNow));

        }
    }
}
