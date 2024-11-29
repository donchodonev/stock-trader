using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using StockTrader.Core.Interfaces;

namespace StockTrader.Prices
{
    public class Prices
    {
        private readonly ILogger _logger;
        private readonly IPriceService _priceService;

        public Prices(ILoggerFactory loggerFactory, IPriceService priceService)
        {
            _logger = loggerFactory.CreateLogger<Prices>();
            _priceService = priceService;
        }

        [Function(nameof(GetNewPrices))]
        public async Task GetNewPrices([TimerTrigger("* * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await _priceService.SendPriceAsync();
        }
    }
}
