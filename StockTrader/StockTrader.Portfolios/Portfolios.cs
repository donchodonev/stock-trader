using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Messages;
using StockTrader.Core.Interfaces;

namespace StockTrader.Portfolios
{
    public class Portfolios(ILogger<Portfolios> logger, IMessageHandler<IMessage<PriceDto>> priceMessageHandler)
    {
        [Function(nameof(GetPortfolio))]
        public async Task<object> GetPortfolio([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "portfolio/{portfolioId}")] HttpRequestData req, string portfolioId)
        {
            logger.LogInformation($"{nameof(GetPortfolio)} is executing.");
            return new { query = req.Query };
        }

        [Function(nameof(ConsumePrice))]
        public async Task ConsumePrice(
            [ServiceBusTrigger(
                MessagingConstants.Topics.PRICES,
                MessagingConstants.Subscriptions.PORTFOLIO,
                Connection = "AzureServiceBusSendListenConnectionString")] PriceMessage message)
        {
            logger.LogInformation($"Received message: {message}");
            await priceMessageHandler.HandleAsync(message);
        }
    }
}
