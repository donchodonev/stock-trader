using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Messages;
using StockTrader.Application.Requests;
using StockTrader.Core.Interfaces;

using System.Text.Json;

namespace StockTrader.Orders
{
    public class Orders(ILogger<Orders> log, IOrderService orderService, IMessageHandler<IMessage<PriceDto>> priceMessageHandler)
    {
        [Function(nameof(PostAsync))]
        public async Task PostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")] HttpRequestData req)
        {
            log.LogInformation($"{nameof(PostAsync)} is executing.");

            var requestBody = await JsonSerializer.DeserializeAsync<StockRequest>(req.Body);
            await orderService.HandleOrderRequestAsync(requestBody);
        }

        [Function(nameof(ConsumePrice))]
        public async Task ConsumePrice(
            [ServiceBusTrigger(
                MessagingConstants.Topics.PRICES,
                MessagingConstants.Subscriptions.ORDER,
                Connection = "AzureServiceBusSendListenConnectionString")] PriceMessage message)
        {
            await priceMessageHandler.HandleAsync(message);
        }
    }
}

