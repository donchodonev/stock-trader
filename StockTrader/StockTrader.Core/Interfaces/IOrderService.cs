using StockTrader.Core.Entities;
using StockTrader.Core.Requests;

namespace StockTrader.Core.Interfaces
{
    public interface IOrderService
    {
        public Task<long> CreateOrderAsync(IStockRequest request);

        public Task<Order> GetOrderAsync(long orderId);
    }
}
