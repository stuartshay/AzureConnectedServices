namespace AzureConnectedServices.Core.Utility
{
    public static class DateHelpers
    {
        public static double TotalDays(DateTime from, DateTime to)
        {
            var startDate = DateTime.SpecifyKind(from, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(to, DateTimeKind.Utc).AddDays(1);

            var days = (endDate - startDate).TotalDays;

            return days;
        }

        public static IEnumerable<Tuple<DateTime, DateTime>> SplitDateRange(DateTime start, DateTime end, int dayChunkSize)
        {
            DateTime chunkEnd;
            while ((chunkEnd = start.AddDays(dayChunkSize)) < end)
            {
                yield return Tuple.Create(start, chunkEnd.AddSeconds(-1));
                start = chunkEnd;
            }
            yield return Tuple.Create(start, end);
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
