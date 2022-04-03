using AzureConnectedServices.GrpcService.Models;
using ProtoBuf.Grpc;

namespace AzureConnectedServices.GrpcService.Services.CodeFirst
{
    /// <summary>
    /// 
    /// </summary>
    public class NoaaWeatherService : INoaaWeatherService
    {
        public Task<SampleCodeFirstReply> UnaryOperation(SampleCodeFirstRequest request, CallContext context = default)
        {
            var content = $"Your request content was '{request.Content}'|{DateTime.Now}";
            return Task.FromResult(new SampleCodeFirstReply { Content = content });
        }

        public Task<SampleCodeFirstReply> WeatherOperation(NoaaWeatherRequest request, CallContext context = default)
        {
            var content = $"Your request content was '{request.StationId}'|{DateTime.Now}";
            return Task.FromResult(new SampleCodeFirstReply { Content = content });
        }
    }
}
