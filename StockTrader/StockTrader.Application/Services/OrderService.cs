using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Factories;
using StockTrader.Core.Entities;
using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;
using StockTrader.Core.Requests;

namespace StockTrader.Application.Services
{
    public class OrderService(ILoggerFactory loggerFactory, IMessageClient messageClient, IRepository<Order> orderRepository) : IOrderService
    {
        private readonly ILogger<OrderService> _logger = loggerFactory.CreateLogger<OrderService>();

        public async Task<long> CreateOrderAsync(IStockRequest request)
        {
            _logger.LogInformation($"Entered {nameof(CreateOrderAsync)} at {DateTime.UtcNow}");

            var order = request.CreateOrder();

            if (request.OrderAction == OrderAction.Invalid)
            {
                return -1;
            }

            await orderRepository.AddAsync(order);

            await messageClient.SendMessageAsync(order.ToOrderMessage(), MessagingConstants.Topics.ORDER_SENT);

            return order.Id;
        }

        public async Task<Order> GetOrderAsync(long orderId)
        {
            return await orderRepository.FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task UpdateOrderAsync(IMessage<OrderReceiptDto> order)
        {

        }
    }
}
