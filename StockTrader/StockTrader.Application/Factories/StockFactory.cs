using StockTrader.Application.DTOs;
using StockTrader.Application.Response;
using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Factories
{
    public static class StockFactory
    {
        public static Stock Create(string ticker, decimal price)
            => new Stock { Ticker = ticker, Price = price };

        public static PortfolioStock ToStock(this IMessage<PriceDto> priceDto)
            => new PortfolioStock { Ticker = priceDto.Payload.Ticker, Price = priceDto.Payload.Price };

        public static StockDto ToStockDto(this PersonStock personStock)
            => new StockDto { Price = personStock.Stock.Price, Quantity = personStock.Quantity, Ticker = personStock.Stock.Ticker };
    }
}
