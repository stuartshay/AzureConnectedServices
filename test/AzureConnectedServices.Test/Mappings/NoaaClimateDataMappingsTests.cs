using AzureConnectedServices.Models.Client;
using AzureConnectedServices.Test.Fixtures;
using MapsterMapper;
using Xunit;
using Xunit.Abstractions;

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
    }
}
