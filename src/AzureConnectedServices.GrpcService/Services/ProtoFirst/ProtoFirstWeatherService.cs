using AzureConnectedServices.Services.Proto;
using Grpc.Core;

namespace AzureConnectedServices.Services.ProtoFirst;

/// <summary>
/// Noaa Weather Service
/// </summary>
public class ProtoFirstWeatherService : NoaaWeather.NoaaWeatherBase
{
    public override async Task<AzureConnectedServices.Services.Proto.SampleProtoFirstReply> NoaaWeatherOperation(NoaaWeatherRequest request, ServerCallContext context)
    {
        var content = $"Your request content was '{request.StationId}'|{DateTime.Now}";
        
        var response = new SampleProtoFirstReply { Content = content };
        return response;
    }

}
