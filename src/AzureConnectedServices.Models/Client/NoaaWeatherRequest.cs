namespace AzureConnectedServices.Models.Client
{
    public class NoaaWeatherRequest
    {
        public string StationId { get; set; }

        public string DataSetId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Limit { get; set; }
    }
}
