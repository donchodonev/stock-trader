using StockTrader.Application.DTOs;
using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Handlers
{
    public class OrderReceiptMessageHandler(IRepository<Order> orderRepository) : IMessageHandler<IMessage<OrderReceiptDto>>
    {
        public async Task HandleAsync(IMessage<OrderReceiptDto> message)
        {
            var order = await orderRepository.FirstOrDefaultAsync(x => x.Id == message.Payload.Id);
            
            order.OrderStatus = message.Payload.OrderStatus;
            order.StatusReason = message.Payload.Reason;
            order.OrderStatus = message.Payload.OrderStatus;

            await orderRepository.SaveChangesAsync();
        }
    }
}
