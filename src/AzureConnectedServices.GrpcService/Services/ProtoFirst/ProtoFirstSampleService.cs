using AzureConnectedServices.Services.Proto;
using Grpc.Core;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;

namespace AzureConnectedServices.Services.ProtoFirst;

/// <summary>
/// ProtoFirstSampleService
/// </summary>
public class ProtoFirstSampleService : ProtoFirstGreeter.ProtoFirstGreeterBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<SampleProtoFirstReply> UnaryOperation(SampleProtoFirstRequest request, ServerCallContext context)
    {
        return Task.FromResult(new SampleProtoFirstReply { Content = $"Your request content was '{request.Content}'" });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="responseStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task ServerStreamingOperation(SampleProtoFirstRequest request, IServerStreamWriter<SampleProtoFirstReply> responseStream, ServerCallContext context)
    {
        return Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(i => new SampleProtoFirstReply { Content = $"Streaming message #{i}. Your request content was '{request.Content}'" })
            .Do(reply => responseStream.WriteAsync(reply))
            .ToTask();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<SampleProtoFirstReply> ClientStreamingOperation(IAsyncStreamReader<SampleProtoFirstRequest> requestStream, ServerCallContext context)
    {
        var messageCount = 0;
        var contentBuilder = new StringBuilder();

        await foreach (var message in requestStream.ReadAllAsync())
        {
            messageCount++;
            contentBuilder.AppendLine(message.Content);
        }

        return new SampleProtoFirstReply()
        {
            Content = $"You sent {messageCount} messages. The content of these messages was:\n{contentBuilder}"
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestStream"></param>
    /// <param name="responseStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task DuplexStreamingOperation(IAsyncStreamReader<SampleProtoFirstRequest> requestStream, IServerStreamWriter<SampleProtoFirstReply> responseStream, ServerCallContext context)
    {
        var messageCount = 0;
        await foreach (var message in requestStream.ReadAllAsync())
        {
            messageCount++;
            await responseStream.WriteAsync(new SampleProtoFirstReply() { Content = $"You have sent {messageCount} requests so far. The most recent request content was {message.Content}" });
        }

    }
}
