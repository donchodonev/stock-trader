using StockTrader.Application.DTOs;
using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Factories
{
    public static class StockFactory
    {
        public static Stock Create(string ticker, decimal price)
            => new Stock { Ticker = ticker, Price = price };

        public static VersionedStock ToStock(this IMessage<PriceDto> priceDto)
            => new VersionedStock { Ticker = priceDto.Payload.Ticker, Price = priceDto.Payload.Price};
    }
}
