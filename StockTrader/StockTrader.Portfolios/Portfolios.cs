using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace StockTrader.Portfolios
{
    public class Portfolios
    {
        private readonly ILogger<Portfolios> _logger;

        public Portfolios(ILogger<Portfolios> logger)
        {
            _logger = logger;
        }

        [Function(nameof(GetAsync))]
        public async Task<object> GetAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "portfolio/{portfolioId}")] HttpRequestData req, string portfolioId)
        {
            _logger.LogInformation($"{nameof(GetAsync)} is executing.");
            return new { query = req.Query };
        }
    }
}
