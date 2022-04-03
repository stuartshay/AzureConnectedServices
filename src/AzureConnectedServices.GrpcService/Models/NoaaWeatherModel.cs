using System.Runtime.Serialization;

namespace AzureConnectedServices.GrpcService.Models
{
    [DataContract]
    public class NoaaWeatherRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 1)]
        public string StationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 2)]
        public string DataSetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 3)]
        public string StartDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 4)]
        public string EndDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 5)]
        public int Limit { get; set; }
    }
}
