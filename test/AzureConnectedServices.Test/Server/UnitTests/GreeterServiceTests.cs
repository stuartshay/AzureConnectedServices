using Moq;
using Tests.Server.UnitTests.Helpers;
using Xunit;

namespace Tests.Server.UnitTests
{
    public class WorkerTests
    {
        #region snippet_SayHelloUnaryTest
        [Fact]
        public async Task SayHelloUnaryTest()
        {
            // Arrange
            //var mockGreeter = new Mock<IGreeter>();

            //mockGreeter.Setup(
            //    m => m.Greet(It.IsAny<string>())).Returns((string s) => $"Hello {s}");
            //var service = new TesterService(mockGreeter.Object);

            //// Act
            //var response = await service.SayHelloUnary(
            //    new HelloRequest { Name = "Joe" }, TestServerCallContext.Create());

            //// Assert
            //mockGreeter.Verify(v => v.Greet("Joe"));
            //Assert.Equal("Hello Joe", response.Message);
        }
        #endregion

    }
}
