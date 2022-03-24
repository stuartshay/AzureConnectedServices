namespace AzureConnectedServices.Models
{
    public class WeatherRequestModel
    {
        public DateTime Date { get; set; }

        public string Datatype { get; set; }

        public string Station { get; set; }

        public string Attributes { get; set; }

        public long Value { get; set; }
    }
}
