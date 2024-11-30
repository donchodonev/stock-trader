namespace StockTrader.Application.DTOs
{
    public class PriceDto(string ticker, decimal price)
    {
        public string Ticker { get; init; } = ticker;

        public decimal Price { get; init; } = price;
    }
}
