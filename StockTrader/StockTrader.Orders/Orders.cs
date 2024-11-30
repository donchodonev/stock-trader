using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Factories;
using StockTrader.Application.Messages;
using StockTrader.Application.Requests;
using StockTrader.Core.Interfaces;

using System.Net;
using System.Text.Json;

namespace StockTrader.Orders
{
    public class Orders(ILogger<Orders> log, IOrderService orderService, IMessageHandler<IMessage<PriceDto>> priceMessageHandler)
    {
        [Function(nameof(PostOrder))]
        public async Task<object> PostOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")] HttpRequestData req)
        {
            log.LogInformation($"{nameof(PostOrder)} is executing.");

            var requestBody = await JsonSerializer.DeserializeAsync<StockRequest>(req.Body);
            var orderId = await orderService.CreateOrderAsync(requestBody);
            return new {OrderId = orderId};
        }

        [Function(nameof(GetOrder))]
        public async Task<HttpResponseData> GetOrder([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order/{orderId}")] HttpRequestData req, long orderId)
        {
            log.LogInformation($"{nameof(GetOrder)} is executing.");

            var order = await orderService.GetOrderAsync(orderId);
            var response = req.CreateResponse(order == null ? HttpStatusCode.NotFound : HttpStatusCode.OK);
            await response.WriteAsJsonAsync(order?.ToDto());

            return response;
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

