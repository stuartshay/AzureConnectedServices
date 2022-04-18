using AzureConnectedServices.Services.Proto;
using Bogus;
using Google.Protobuf.Collections;

namespace AzureConnectedServices.Test.Data
{
    public static class ClimateProtoDataSet
    { 
        static ClimateProtoDataSet()
        {
            Faker.GlobalUniqueIndex = 0;
        }

        public static NoaaClimateDataResponse GetClimateDataResult(int count)
        {
            var resultFaker = new Faker<Result>()
                //.RuleFor(c => c.Date, f => f.Date.Past(1))
                .RuleFor(c => c.Value, f => f.Random.Int(-10, 110))
                .RuleFor(c => c.Datatype, f => f.Lorem.Word())
                .RuleFor(c => c.Attributes, f => f.Lorem.Word())
                .RuleFor(c => c.Station, f => f.Address.StateAbbr());

            var results = resultFaker.Generate(count);

            RepeatedField<Services.Proto.Result> list = new RepeatedField<Services.Proto.Result>();
            //list.Add(new Result{Attributes = "A", Datatype = "B", Station = "C", Value = 999});
            //list.Add(new Result { Attributes = "A", Datatype = "B", Station = "C", Value = 999 });

            NoaaClimateDataResponse response = new NoaaClimateDataResponse
            {
                Result = {  },
                Metadata = new Metadata { Resultset = new Resultset { Count = count, Limit = 100, Offset = 0 } }
            };

            return response;
        }

    }
}
