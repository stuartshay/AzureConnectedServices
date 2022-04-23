using AzureConnectedServices.Models.Client;
using AzureConnectedServices.Test.Data;
using AzureConnectedServices.WebApi.Controllers;
using AzureConnectedServices.WebApi.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace AzureConnectedServices.Test.Controllers
{
    public class ClimateDataControllerTests
    {
        private readonly ITestOutputHelper _output;

        public ClimateDataControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "TODO")]
        public void Given_NullParameter_Constructor_ShouldThrow_ArgumentNullException()
        {
            // Act
            Action action = () =>
            {
                var climateDataController = new ClimateDataController(null, null);
            };

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task Get_ClimateData_ReturnsData()
        {
            // Arrange 
            var dataSet = ClimateDataSet.GetClimateDataResult(10);

            var mockClimateDataService = new Mock<IClimateDataService>();
            mockClimateDataService.Setup(b => b.GetClimateData(It.IsAny<NoaaClimateDataRequest>()))
                .ReturnsAsync(dataSet);

            var controller = GetClimateDataController(mockClimateDataService.Object);

            // Act
            var sut = await controller.PostGrpc(It.IsAny<NoaaClimateDataRequest>());

            //Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);

            Assert.IsType<ClimateDataResult>(objectResult.Value);
        }

        private ClimateDataController GetClimateDataController(
            IClimateDataService climateDataService = null)
        {
            climateDataService ??= new Mock<IClimateDataService>().Object;
            var logger = new Mock<ILogger<ClimateDataController>>().Object;

            return new ClimateDataController(climateDataService, logger);
        }
    }
}
