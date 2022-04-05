using System.Runtime.Serialization;

namespace AzureConnectedServices.Models.Client
{
    [DataContract]
    public class NoaaWeatherRequest
    {
        /// <summary>
        /// Station Id
        /// </summary>
        [DataMember(Order = 1)]
        public string StationId { get; set; }

        /// <summary>
        /// DataSet Id
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
