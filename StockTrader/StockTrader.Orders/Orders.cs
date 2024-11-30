using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;

using System.Text.Json;

namespace StockTrader.Orders
{
    public class Orders
    {
        private readonly ILogger<Orders> _logger;

        public Orders(ILogger<Orders> log)
        {
            _logger = log;
        }

        [Function(nameof(PostAsync))]
        public async Task<object> PostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")] HttpRequestData req)
        {
            _logger.LogInformation($"{nameof(PostAsync)} is executing.");
            var requestBody = await JsonSerializer.DeserializeAsync<object>(req.Body);
            return requestBody;
        }

        [Function(nameof(ConsumePrice))]
        public async Task ConsumePrice(
            [ServiceBusTrigger(
                        MessagingConstants.Topics.PRICES,
                        MessagingConstants.Subscriptions.PORTFOLIO,
                        Connection = "AzureServiceBusSendListenConnectionString")] string message)
                {
                    _logger.LogInformation($"Received message: {message}");
                }
    }
}

