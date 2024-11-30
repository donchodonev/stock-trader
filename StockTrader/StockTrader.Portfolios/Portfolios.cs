using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Messages;
using StockTrader.Application.Response;
using StockTrader.Core.Interfaces;

namespace StockTrader.Portfolios
{
    public class Portfolios(
        ILogger<Portfolios> logger,
        IMessageHandler<IMessage<PriceDto>> priceMessageHandler,
        IMessageHandler<IMessage<OrderDto>> orderMessageHandler,
        IPortfolioService portfolioService)
    {
        [Function(nameof(GetPortfolio))]
        public async Task<PortfolioResponse> GetPortfolio([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "person/{personId}/portfolio")] HttpRequestData req, int personId)
        {
            var personStocks = await portfolioService.GetPersonPortfolioAsync(personId);
            var response = personStocks.Select(x => new StockDto { Price = x.Stock.Price, Quantity = x.Quantity, Ticker = x.Stock.Ticker }).ToList();
            return new PortfolioResponse { Stocks = response }; //not the best idea but no time for fluent result setup
        }

        [Function(nameof(ConsumePrice))]
        public async Task ConsumePrice(
            [ServiceBusTrigger(
                MessagingConstants.Topics.PRICES,
                MessagingConstants.Subscriptions.PORTFOLIO,
                Connection = "AzureServiceBusSendListenConnectionString")] PriceMessage message)
        {
            await priceMessageHandler.HandleAsync(message);
        }

        [Function(nameof(ConsumeOrder))]
        public async Task ConsumeOrder(
        [ServiceBusTrigger(
            MessagingConstants.Topics.ORDER_SENT,
                MessagingConstants.Subscriptions.PORTFOLIO,
                Connection = "AzureServiceBusSendListenConnectionString")] OrderMessage message)
        {
            await orderMessageHandler.HandleAsync(message);
        }
    }
}
