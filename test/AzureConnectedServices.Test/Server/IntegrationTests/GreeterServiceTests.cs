//using System.Threading.Channels;
//using Grpc.Core;
////using Server;
////using Test;
//using Tests.Server.IntegrationTests.Helpers;
//using Xunit;
//using Xunit.Abstractions;

//namespace Tests.Server.IntegrationTests
//{
//    public class GreeterServiceTests : IntegrationTestBase
//    {
//        //public GreeterServiceTests(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper)
//        //    : base(fixture, outputHelper)
//        //{
//        //}

//        //#region snippet_SayHelloUnaryTest
//        //[Fact]
//        //public async Task SayHelloUnaryTest()
//        //{
//        //    // Arrange
//        //    var client = new Tester.TesterClient(Channel);

//        //    // Act
//        //    var response = await client.SayHelloUnaryAsync(new HelloRequest { Name = "Joe" });

//        //    // Assert
//        //    Assert.Equal("Hello Joe", response.Message);
//        //}
//        //#endregion

//        [Fact]
//        public async Task SayHelloClientStreamingTest()
//        {
//            // Arrange
//            var client = new Tester.TesterClient(Channel);

//            var names = new[] { "James", "Jo", "Lee" };
//            HelloReply response;

//            // Act
//            using var call = client.SayHelloClientStreaming();
//            foreach (var name in names)
//            {
//                await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
//            }
//            await call.RequestStream.CompleteAsync();

//            response = await call;

//            // Assert
//            Assert.Equal("Hello James, Jo, Lee", response.Message);
//        }

//    }
//}
