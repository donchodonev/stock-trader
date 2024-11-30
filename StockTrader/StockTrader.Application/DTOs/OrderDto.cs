using StockTrader.Core.Enums;

namespace StockTrader.Application.DTOs
{
    public class OrderDto(long id, int personId, string ticker, int quantity, OrderStatus orderStatus, OrderAction orderAction, string statusReason)
    {
        public long Id { get; set; } = id;

        public int PersonId { get; set; } = personId;

        public string Ticker { get; set; } = ticker;

        public int Quantity { get; set; } = quantity;

        public OrderAction OrderAction { get; set; } = orderAction;

        public OrderStatus OrderStatus { get; set; } = orderStatus;

        public string StatusReason { get; set; } = statusReason;
    }
}
