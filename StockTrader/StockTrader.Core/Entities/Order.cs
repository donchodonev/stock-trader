using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;

namespace StockTrader.Core.Entities
{
    public class Order : IAuditable
    {
        public long Id { get; set; }

        public int PersonId { get; set; }

        public string Ticker { get; set; }

        public int Quantity { get; set; }

        public OrderAction OrderAction { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string StatusReason { get; set; }
    }
}
