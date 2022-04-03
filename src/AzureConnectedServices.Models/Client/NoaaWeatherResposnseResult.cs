using System.Text.Json.Serialization;

namespace AzureConnectedServices.Models.Client
{

        public class WeatherResult
        {
            [JsonPropertyName("metadata")]
            public Metadata Metadata { get; set; }

            [JsonPropertyName("results")]
            public Result[] Results { get; set; }
        }

        public class Metadata
        {
            [JsonPropertyName("resultset")]
            public Resultset Resultset { get; set; }
        }

        public class Resultset
        {
            [JsonPropertyName("offset")]
            public long Offset { get; set; }

            [JsonPropertyName("count")]
            public long Count { get; set; }

            [JsonPropertyName("limit")]
            public long Limit { get; set; }
        }

        public class Result
        {
            [JsonPropertyName("date")]
            public DateTime Date { get; set; }

            [JsonPropertyName("datatype")]
            public string Datatype { get; set; }

            [JsonPropertyName("station")]
            public string Station { get; set; }

            [JsonPropertyName("attributes")]
            public string Attributes { get; set; }

            [JsonPropertyName("value")]
            public long Value { get; set; }
        }
}
