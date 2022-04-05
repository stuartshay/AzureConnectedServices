using AzureConnectedServices.GrpcService.Models;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace AzureConnectedServices.GrpcService.Services;

/// <summary>
/// This is a sample service that demonstrates all types of gRPC operations from a code-first gRPC service
/// </summary>
[ServiceContract]
public interface ICodeFirstGreeterService
{
    /// <summary>
    /// A Unary Void operation takes a single request, and does not return a response
    /// </summary>
    [OperationContract]
    Task UnaryVoidOperation(SampleCodeFirstRequest request, CallContext context = default);

    /// <summary>
    /// A Unary operation takes a single request, and returns a single response
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [OperationContract]
    Task<SampleCodeFirstReply> UnaryOperation(SampleCodeFirstRequest request, CallContext context = default);

    /// <summary>
    /// A Server Streaming operation takes a single request, and returns a stream of zero or more responses
    /// </summary>
    [OperationContract]
    IAsyncEnumerable<SampleCodeFirstReply> ServerStreamingOperation(SampleCodeFirstRequest request, CallContext context = default);

    /// <summary>
    /// A Client Streaming operation takes a stream of one or more requests, and returns a single response when the request stream is closed
    /// </summary>
    [OperationContract]
    Task<SampleCodeFirstReply> ClientStreamingOperation(IAsyncEnumerable<SampleCodeFirstRequest> request, CallContext context = default);

    /// <summary>
    /// A Duplex operation take a stream of zero or more requests, and returns a stream of zero or more responses
    /// </summary>
    [OperationContract]
    IAsyncEnumerable<SampleCodeFirstReply> DuplexStreamingOperation(IAsyncEnumerable<SampleCodeFirstRequest> request, CallContext context = default);
}
