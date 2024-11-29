using Azure.Messaging.ServiceBus;

using Microsoft.Extensions.Logging;

using StockTrader.Application.Extensions;
using StockTrader.Core.Interfaces;

namespace StockTrader.Infrastructure.Clients
{
    public class AzureServiceBusClient : IMessageClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger _logger;

        public AzureServiceBusClient(ILoggerFactory loggerFactory, ServiceBusClient serviceBusClient)
        {
            if (_serviceBusClient == null)
            {
                _serviceBusClient = serviceBusClient;
            }

            if(_logger == null)
            {
                _logger = loggerFactory.CreateLogger<AzureServiceBusClient>();
            }
        }

        public async Task SendMessageAsync<T>(IMessage<T> message, string topicName)
        {
            _logger.LogInformation($"Entered {nameof(SendMessageAsync)} at {DateTime.Now}");

            if (message == null) throw new ArgumentException("Message cannot be null.");
            if (string.IsNullOrWhiteSpace(topicName)) throw new ArgumentException("Topic name cannot be null or empty.");

            await using var sender = _serviceBusClient.CreateSender(topicName);
            await sender.SendMessageAsync(message.ToServiceBusMessage());
        }
    }
}
