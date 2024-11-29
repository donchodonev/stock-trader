using StockTrader.Application.Messages;

namespace StockTrader.Application.Factories
{
    public static class MessageFactory
    {
        public static NewPriceMessage CreateNewPriceMessage(string ticker, decimal price)
            => new NewPriceMessage(Core.Enums.MessageSource.PriceService, (ticker, price));
    }
}
