using StockTrader.Core.Enums;

namespace StockTrader.Application.DTOs
{
    public class OrderDto(long id, int personId, string ticker, int quantity, OrderStatus orderStatus)
    {
        public long Id { get; set; } = id;

        public int PersonId { get; set; } = personId;

        public string Ticker { get; set; } = ticker;

        public int Quantity { get; set; } = quantity;

        public OrderStatus OrderStatus { get; set; } = orderStatus;
    }
}
