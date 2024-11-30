using StockTrader.Core.Enums;

namespace StockTrader.Core.Requests
{
    public interface IStockRequest
    {
        public int PersonId { get; set; }

        public string Ticker { get; set; }

        public int Quantity { get; set; }

        public OrderAction OrderAction { get; set; }
    }
}
