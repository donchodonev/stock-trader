using StockTrader.Core.Requests;

namespace StockTrader.Core.Interfaces
{
    public interface IOrderService
    {
        public Task HandleOrderRequestAsync(IStockRequest request);
    }
}
