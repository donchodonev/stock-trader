using StockTrader.Application.DTOs;
using StockTrader.Core.Entities;
using StockTrader.Core.Enums;
using StockTrader.Core.Requests;

namespace StockTrader.Application.Factories
{
    public static class OrderFactory
    {
        public static Order CreateOrder(this IStockRequest request)
            => new Order
            {
                PersonId = request.PersonId,
                Quantity = request.Quantity,
                Ticker = request.Ticker,
                OrderStatus = OrderStatus.InProgress,
            };

        public static OrderDto ToDto(this Order order)
            => new OrderDto(order.Id, order.PersonId, order.Ticker, order.Quantity, order.OrderStatus);
    }
}
