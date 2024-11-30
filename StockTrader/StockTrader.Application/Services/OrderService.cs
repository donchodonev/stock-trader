using Microsoft.Extensions.Logging;

using StockTrader.Application.Constants;
using StockTrader.Application.Factories;
using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;
using StockTrader.Core.Requests;

namespace StockTrader.Application.Services
{
    public class OrderService(ILoggerFactory loggerFactory, IMessageClient messageClient, IRepository<Order> orderRepository) : IOrderService
    {
        private readonly ILogger<OrderService> _logger = loggerFactory.CreateLogger<OrderService>();

        public async Task HandleOrderRequestAsync(IStockRequest request)
        {
            _logger.LogInformation($"Entered {nameof(HandleOrderRequestAsync)} at {DateTime.UtcNow}");

            var order = request.CreateOrder();
            await orderRepository.AddAsync(order);

            await messageClient.SendMessageAsync(order.ToOrderMessage(), MessagingConstants.Topics.ORDER_SENT);
        }
    }
}
