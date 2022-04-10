//using Grpc.Net.Client;
//using Microsoft.Extensions.Logging;
//using Server;
//using Tests.Server.IntegrationTests.Helpers;
//using Xunit;
//using Xunit.Abstractions;

//namespace Tests.Server.IntegrationTests
//{
//    public class IntegrationTestBase : IClassFixture<GrpcTestFixture<Startup>>, IDisposable
//    {
//        private GrpcChannel? _channel;
//        private IDisposable? _testContext;

//        protected GrpcTestFixture<Startup> Fixture { get; set; }

//        protected ILoggerFactory LoggerFactory => Fixture.LoggerFactory;

//        protected GrpcChannel Channel => _channel ??= CreateChannel();

//        protected GrpcChannel CreateChannel()
//        {
//            return GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
//            {
//                LoggerFactory = LoggerFactory,
//                HttpHandler = Fixture.Handler
//            });
//        }

//        public IntegrationTestBase(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper)
//        {
//            Fixture = fixture;
//            _testContext = Fixture.GetTestContext(outputHelper);
//        }

//        public void Dispose()
//        {
//            _testContext?.Dispose();
//            _channel = null;
//        }
//    }
//}
