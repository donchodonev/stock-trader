using StockTrader.Application.Messages;
using StockTrader.Core.Entities;

namespace StockTrader.Application.Factories
{
    public static class MessageFactory
    {
        public static NewPriceMessage ToNewPriceMessage(this Stock stock)
            => new NewPriceMessage(Core.Enums.MessageSource.PriceService, (stock.Ticker, stock.Price));
    }
}
