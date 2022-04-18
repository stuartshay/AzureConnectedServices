namespace AzureConnectedServices.Test.Utility
{
    using System;
    using System.Linq;
    using AzureConnectedServices.Core.Utility;
    using Xunit;
    using Xunit.Abstractions;

    namespace WeatherWorker.Tests
    {
        public class DateHelpersTest
        {
            private readonly ITestOutputHelper _testOutputHelper;

            public DateHelpersTest(ITestOutputHelper testOutputHelper)
            {
                _testOutputHelper = testOutputHelper;
            }

            [Fact]
            public void TotalDays()
            {
                var startDate = DateTime.Parse("2005-01-01");
                var endDate = DateTime.Parse("2005-12-31");

                var totalDays = DateHelpers.TotalDays(DateTime.SpecifyKind(startDate, DateTimeKind.Utc), DateTime.SpecifyKind(endDate, DateTimeKind.Utc));
                Assert.Equal(365, totalDays);
            }

            [Theory]
            //[InlineData("2005-01-01", "2005-12-31")]
            //[InlineData("2022-04-01", "2022-04-30")]
            [InlineData("2022-02-01", "2022-04-28")]
            public void BatchRequests(string startDate, string endDate)
            {
                var startDateTime = DateTime.Parse(startDate);
                var endDateTime = DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1);

                var batchSize = 5;
                int numberOfBatches = (int)Math.Ceiling((DateHelpers.TotalDays(startDateTime, endDateTime) / batchSize));

                var batches = DateHelpers.SplitDateRange(startDateTime, endDateTime, batchSize);

                //Assert.Equal(numberOfBatches, batches.ToList().Count());
                foreach (var batch in batches)
                {
                    _testOutputHelper.WriteLine($"{batch.Item1}-{batch.Item2}");
                }

            }
        }
    }
}
