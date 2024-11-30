using StockTrader.Application.DTOs;
using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Messages
{
    public class OrderMessage(MessageSource messageSource, OrderDto payload) : IMessage<OrderDto>
    {
        public MessageSource MessageSource { get; init; } = messageSource;

        public OrderDto Payload { get; init; } = payload;
    }
}
