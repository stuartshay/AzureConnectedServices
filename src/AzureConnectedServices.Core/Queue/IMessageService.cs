using AzureConnectedServices.Models.Message;

namespace AzureConnectedServices.Core.Queue
{
    public interface IMessageService
    {
        Task SendAsync<TMessageType>(TMessageType message, string queueName, MessageMetadata messageMetadata);
    }
}
