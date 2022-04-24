using AzureConnectedServices.Core.Queue;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace AzureConnectedServices.Test.Fixtures
{
    public class AzureServiceBusServiceFixture
    {
        public IMessageService MessageService;

        public AzureServiceBusServiceFixture()
        {
            var services = new ServiceCollection();
            
            var connection = Environment.GetEnvironmentVariable("SERVICE_BUS_APPCONFIG", EnvironmentVariableTarget.Machine);
            services.AddAzureClients(a =>
            {
                a.AddServiceBusClient(connection);
            });

            services.AddTransient<IMessageService, AzureServiceBusService>();
            var sp = services.BuildServiceProvider();

            MessageService = sp.GetRequiredService<IMessageService>();
        }
    }
}
