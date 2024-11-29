using StockTrader.Core.Entities;

namespace StockTrader.Application.Factories
{
    public static class StockFactory
    {
        public static Stock Create(string ticker, decimal price)
            => new Stock { Ticker = ticker, Price = price };
    }
}
