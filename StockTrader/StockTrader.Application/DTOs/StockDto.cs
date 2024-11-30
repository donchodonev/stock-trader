namespace StockTrader.Application.DTOs
{
    public class StockDto
    {
        public string Ticker { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
