using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AzureConnectedServices.Models.Client
{
    [DataContract]
    public class WeatherResult
    {
        [DataMember(Order = 1)]
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [DataMember(Order = 2)]
        [JsonPropertyName("results")]
        public Result[] Results { get; set; }
    }

    [DataContract]
    public class Metadata
    {
        [DataMember(Order = 1)]
        [JsonPropertyName("resultset")]
        public Resultset Resultset { get; set; }
    }

    [DataContract]
    public class Resultset
    {
        [DataMember(Order = 1)]
        [JsonPropertyName("offset")]
        public long Offset { get; set; }

        [DataMember(Order = 2)]
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [DataMember(Order = 3)]
        [JsonPropertyName("limit")]
        public long Limit { get; set; }
    }

    [DataContract]
    public class Result
    {
        [DataMember(Order = 1)]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [DataMember(Order = 2)]
        [JsonPropertyName("datatype")]
        public string Datatype { get; set; }

        [DataMember(Order = 3)]
        [JsonPropertyName("station")]
        public string Station { get; set; }

        [DataMember(Order = 4)]
        [JsonPropertyName("attributes")]
        public string Attributes { get; set; }

        [DataMember(Order = 5)]
        [JsonPropertyName("value")]
        public long Value { get; set; }
    }
}
