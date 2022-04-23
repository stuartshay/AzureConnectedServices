using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

namespace AzureConnectedServices.WebApi.Mappings
{
    public static class NoaaClimateDataTransformations
    {
        public static void MapResultsToProto(RepeatedField<AzureConnectedServices.Services.Proto.Result> results, IEnumerable<AzureConnectedServices.Models.Client.Result> items)
        {
            foreach (var item in items)
            {
                AzureConnectedServices.Services.Proto.Result result = MapResultsToProto(item);
                results.Add(result);
            }
        }

        public static AzureConnectedServices.Services.Proto.Result MapResultsToProto(AzureConnectedServices.Models.Client.Result item)
        {
            var result = new AzureConnectedServices.Services.Proto.Result
            {
                Datatype = item.Datatype,
                Station = item.Station,
                Attributes = item.Attributes,
                Value = Convert.ToInt32(item.Value),
                Date = Timestamp.FromDateTime(DateTime.SpecifyKind(item.Date, DateTimeKind.Utc)),
            };

            return result;
        }
    }
}
