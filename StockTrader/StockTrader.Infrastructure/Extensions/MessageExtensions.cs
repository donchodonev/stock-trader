using Azure.Messaging.ServiceBus;

using Newtonsoft.Json;

using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Extensions
{
    public static class MessageExtensions
    {
        public static ServiceBusMessage ToServiceBusMessage<T>(this IMessage<T> message)
            => new ServiceBusMessage(JsonConvert.SerializeObject(message));
    }
}
