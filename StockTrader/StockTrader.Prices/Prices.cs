using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace StockTrader.Prices
{
    public class Prices
    {
        private readonly ILogger _logger;

        public Prices(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Prices>();
        }

        [Function(nameof(GetNewPrices))]
        public void GetNewPrices([TimerTrigger("* * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
