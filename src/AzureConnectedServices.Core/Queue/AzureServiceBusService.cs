﻿using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using AzureConnectedServices.Models.Message;
using Microsoft.Extensions.Logging;

namespace AzureConnectedServices.Core.Queue
{
    public class AzureServiceBusService : IMessageService
    {
        private readonly ServiceBusClient _serviceBusClient;

        public AzureServiceBusService(
            ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;

        }

        public async Task SendAsync<TMessageType>(TMessageType message, string queueName,
            MessageMetadata messageMetadata)
        {
            string json = JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
            });

            ServiceBusMessage queueMessage = new ServiceBusMessage((ReadOnlyMemory<byte>)Encoding.UTF8.GetBytes(json))
            {
                MessageId =  Guid.NewGuid().ToString(),
                CorrelationId = Guid.NewGuid().ToString(),
            };

            ServiceBusSender sender = _serviceBusClient.CreateSender(queueName);
            await sender.SendMessageAsync(queueMessage);

        }

    }
}
