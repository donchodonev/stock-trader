using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.Factories;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Services
{
    public class PriceService : IPriceService
    {
        private readonly ILogger _logger;
        private IMessageClient _messageClient;

        public PriceService(ILoggerFactory loggerFactory, IMessageClient messageClient)
        {
            _logger = loggerFactory.CreateLogger<PriceService>();
            _messageClient = messageClient;
        }

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

            var ticker = _stockTickers[Random.Shared.Next(0, _stockTickers.Length)];
            var price = decimal.Round((decimal)(Random.Shared.NextDouble() * 32.56), 4);
            var message = MessageFactory.CreateNewPriceMessage(ticker, price);

            await _messageClient.SendMessageAsync(message, MessageClientTopics.PRICES_TOPIC);
        }
    }
}
