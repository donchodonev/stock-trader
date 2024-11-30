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
    }
}
