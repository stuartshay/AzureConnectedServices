using AzureConnectedServices.GrpcService.Models;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace AzureConnectedServices.GrpcService.Services.CodeFirst
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface INoaaWeatherService
    {
        /// <summary>
        /// A Unary operation takes a single request, and returns a single response
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        Task<SampleCodeFirstReply> UnaryOperation(SampleCodeFirstRequest request, CallContext context = default);

        /// <summary>
        /// Noaa Weather Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        Task<SampleCodeFirstReply> WeatherOperation(NoaaWeatherRequest request, CallContext context = default);
    }
}
