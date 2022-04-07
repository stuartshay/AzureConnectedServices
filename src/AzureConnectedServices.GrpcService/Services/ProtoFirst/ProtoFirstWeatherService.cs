using AzureConnectedServices.Services.Proto;
using Grpc.Core;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;

namespace AzureConnectedServices.Services.ProtoFirst;

/// <summary>
/// ProtoFirstSampleService
/// </summary>
public class ProtoFirstWeatherService : NoaaWeather.NoaaWeatherBase
{
    public override async Task<SampleProtoFirstReply> NoaaWeatherOperation(NoaaWeatherRequest request, ServerCallContext context)
    {
        var content = $"Your request content was '{request.StationId}'|{DateTime.Now}";

        //Request
        var x = new SampleProtoFirstRequest { Content = "" };

        //Reply
        var y = new SampleProtoFirstReply { Content = "" };

        return y;
    }

}
