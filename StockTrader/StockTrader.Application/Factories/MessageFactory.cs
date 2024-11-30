using StockTrader.Application.DTOs;
using StockTrader.Application.Messages;
using StockTrader.Core.Entities;
using StockTrader.Core.Enums;

namespace StockTrader.Application.Factories
{
    public static class MessageFactory
    {
        public static PriceMessage ToPriceMessage(this Stock stock)
            => new PriceMessage(MessageSource.PriceService, new PriceDto(stock.Ticker, stock.Price));
    }
}
