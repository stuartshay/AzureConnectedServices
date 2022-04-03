namespace AzureConnectedServices.Models
{
    public class WeatherRequestModel
    {
        /// <summary>
        /// Start Date
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Data Set Id
        /// </summary>
        public string DataSetId { get; set; }

        /// <summary>
        /// Station Id
        /// </summary>
        public string StationId { get; set; }

        /// <summary>
        /// Results Limit
        /// </summary>
        public long Limit { get; set; }

        public override string ToString()
        {
            return $"StartDate:{StartDate}, EndDate:{EndDate}, DataSetId:{DataSetId}, StationId:{StationId}, Limit:{Limit}";
        }
    }
}
