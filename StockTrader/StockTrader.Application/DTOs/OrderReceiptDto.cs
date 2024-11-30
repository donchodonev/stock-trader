using StockTrader.Core.Enums;

namespace StockTrader.Application.DTOs
{
    public class OrderReceiptDto(long id, OrderStatus orderStatus, string reason)
    {
        public long Id { get; set; } = id;

        public OrderStatus OrderStatus { get; set; } = orderStatus;

        public string Reason { get; set; } = reason;
    }
}
