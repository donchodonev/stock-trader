using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Messages
{
    public class NewPriceMessage : IMessage<(string Price, decimal Ticker)>
    {
        public NewPriceMessage(MessageSource messageSource, (string Ticker, decimal Price) payload)
        {
            MessageSource = messageSource;
            Payload = payload;
        }

        public MessageSource MessageSource { get; init; }

        public (string, decimal) Payload { get; init; }
    }
}
