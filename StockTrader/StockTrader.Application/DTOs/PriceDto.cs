namespace StockTrader.Application.DTOs
{
    public class PriceDto
    {
        public PriceDto(string ticker, decimal price)
        {
            Ticker = ticker;
            Price = price;
        }

        public string Ticker { get; init; }

        public decimal Price { get; init; }
    }
}
