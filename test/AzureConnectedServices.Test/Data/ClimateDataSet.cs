using AzureConnectedServices.Models.Client;
using Bogus;

namespace AzureConnectedServices.Test.Data
{
    public static class ClimateDataSet
    {
        static ClimateDataSet()
        {
            Faker.GlobalUniqueIndex = 0;
        }

        public static ClimateDataResult GetClimateDataResult(int count)
        {
            var resultFaker = new Faker<Result>()
                .RuleFor(c => c.Date, f => f.Date.Past(1))
                .RuleFor(c => c.Value, f => f.Random.Int(-10, 110))
                .RuleFor(c => c.Datatype, f => f.Lorem.Word())
                .RuleFor(c => c.Attributes, f => f.Lorem.Word())
                .RuleFor(c => c.Station, f => f.Address.StateAbbr());

            Result[] results = resultFaker.Generate(count).ToArray();
            
            ClimateDataResult result = new ClimateDataResult
            {
                Results = results,
                Metadata = new Metadata{Resultset = new Resultset{Count = count, Limit = 100, Offset = 0}}
            };

            return result;
        }
    }

}
