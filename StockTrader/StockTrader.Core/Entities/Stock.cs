using StockTrader.Core.Interfaces;

namespace StockTrader.Core.Entities
{
    public class Stock : IAuditable
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
