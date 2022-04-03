using System.Runtime.Serialization;

namespace AzureConnectedServices.GrpcService.Models
{
    [DataContract]
    public class SampleCodeFirstRequest
    {
        [DataMember(Order = 1)]
        public string Content { get; set; }
    }

    [DataContract]
    public class SampleCodeFirstReply
    {
        [DataMember(Order = 1)]
        public string Content { get; set; }
    }

}
