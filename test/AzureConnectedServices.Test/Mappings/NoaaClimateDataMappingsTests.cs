using AzureConnectedServices.Services.Proto;
using AzureConnectedServices.Test.Data;
using AzureConnectedServices.Test.Fixtures;
using AzureConnectedServices.WebApi.Mappings;
using MapsterMapper;
using Xunit;
using Xunit.Abstractions;
using NoaaClimateDataRequest = AzureConnectedServices.Models.Client.NoaaClimateDataRequest;
using Result = AzureConnectedServices.Models.Client.Result;
using Resultset = AzureConnectedServices.Services.Proto.Resultset;

namespace AzureConnectedServices.Test.Mappings
{
    public class NoaaClimateDataMappingsTests : IClassFixture<WebApiServicesFixture>
    {
        private readonly ITestOutputHelper _output;

        private readonly IMapper _mapper;

        public NoaaClimateDataMappingsTests(ITestOutputHelper output, WebApiServicesFixture fixture)
        {
            _output = output;
            _mapper = fixture.Mapper;
        }

        [Fact]
        [Trait("Category", "Mappings")]
        public void Validate_Request_Mappings()
        {
            // Arrange
            NoaaClimateDataRequest request = new NoaaClimateDataRequest
            {
                DataSetId = "1212",
                StartDate = "01-01-2022",
                EndDate = "01-10-2022",
                Limit = 100,
                StationId = "GW102AZ",
            };

            // Act
            var requestMapped = _mapper.Map<Services.Proto.NoaaClimateDataRequest>(request);
           
            // Assert
            Assert.Equal(request.DataSetId, requestMapped.DataSetId);
            Assert.Equal(request.StartDate, requestMapped.StartDate);
            Assert.Equal(request.EndDate, requestMapped.EndDate);
            Assert.Equal(request.Limit, requestMapped.Limit);
            Assert.Equal(request.StationId, requestMapped.StationId);
        }

        [Fact]
        [Trait("Category", "Mappings")]
        public void Validate_Result_Mappings()
        {
            // Arrange
            Result result = new Result
            {
                Datatype = "NYZ122",
                Station = "ABZ21",
                Attributes = "2323,,,,01",
                Date = DateTime.UtcNow,
                Value = 23721,
            };

            // Act
            var resultMapped = _mapper.Map<Services.Proto.Result>(result);

            // Assert
            Assert.IsType<Services.Proto.Result>(resultMapped);
            Assert.Equal(result.Datatype, resultMapped.Datatype);
            Assert.Equal(result.Station, resultMapped.Station);
            Assert.Equal(result.Attributes, resultMapped.Attributes);
            Assert.Equal(result.Date, resultMapped.Date.ToDateTime());
        }

        [Fact]
        [Trait("Category", "Mappings")]
        public void Validate_Result_RepeatedField_Mappings()
        {
            var count = 100;
            
            var data = ClimateDataSet.GetClimateDataResult(count).Results;
            var metadata = new Services.Proto.Metadata 
                { Resultset = new Resultset { Count = count, } };

            foreach (var item in data)
            {
                var resultMapped = _mapper.Map<Services.Proto.Result>(item);
                Assert.IsType<Services.Proto.Result>(resultMapped);
                Assert.Equal(item.Datatype, resultMapped.Datatype);
            }

            NoaaClimateDataResponse response = new NoaaClimateDataResponse
            {
                Metadata = metadata,
            };
            NoaaClimateDataTransformations.MapResultsToProto(response.Result, data );

            Assert.IsType<Services.Proto.NoaaClimateDataResponse>(response);
            Assert.Equal(count, response.Result.Count);

        }


    }
}
