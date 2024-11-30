using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.Factories;
using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Services
{
    public class StockService(ILoggerFactory loggerFactory, IMessageClient messageClient, IRepository<Stock> stockRepository) : IStockService
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<StockService>();
        private readonly IMessageClient _messageClient = messageClient;
        private readonly IRepository<Stock> _stockRepository = stockRepository;
        private readonly string[] _stockTickers = [
            "AAPL",
            "NVDA",
            "MSFT",
            "AMZN",
            "META",
            "GOOG",
            "TSLA",
            "LLY",
            "AVGO",
            "V",
            "MA",
            "PG",
            "JNJ",
            "XOM",
            "WMT",
            "HD",
            "COST",
            "UNH"
            ];

        public async Task SendPriceAsync()
        {
            _logger.LogInformation($"{nameof(SendPriceAsync)} executed at {DateTime.Now}");

            var tickerIndex = Random.Shared.Next(0, _stockTickers.Length);
            var ticker = _stockTickers[tickerIndex];

            var priceSeed = 32.56;
            var price = decimal.Round((decimal)(Random.Shared.NextDouble() * priceSeed), 4);

            var stock = StockFactory.Create(ticker, price);
            await _stockRepository.AddAsync(stock);

            var message = stock.ToPriceMessage();
            await _messageClient.SendMessageAsync(message, MessagingConstants.Topics.PRICES);
        }
    }
}
