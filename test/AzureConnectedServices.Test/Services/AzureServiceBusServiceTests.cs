using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureConnectedServices.Core.Configuration;
using AzureConnectedServices.Core.Constants;
using AzureConnectedServices.Core.Queue;
using AzureConnectedServices.Models.Client;
using AzureConnectedServices.Test.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace AzureConnectedServices.Test.Services
{
    public class AzureServiceBusServiceTests : IClassFixture<AzureServiceBusServiceFixture>
    {
        private readonly ITestOutputHelper _output;

        private readonly IMessageService _messageService;

        public AzureServiceBusServiceTests(ITestOutputHelper output, AzureServiceBusServiceFixture fixture)
        {
            _output = output;
            _messageService = fixture.MessageService;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Send_Message_Sucess()
        {
            var request = new NoaaClimateDataRequest
            {
                DataSetId = "1212",
                StartDate = "01-01-2022",
                EndDate = "01-10-2022",
                Limit = 100,
                StationId = "GW102AZ",
            };

            _messageService.SendAsync(request, SettingsConstant.ClimateDataQueue, null);

            Assert.True(true);
        }


    }
}
