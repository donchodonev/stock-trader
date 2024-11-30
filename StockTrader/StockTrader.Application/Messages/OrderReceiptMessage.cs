using StockTrader.Application.DTOs;
using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Messages
{
    public class OrderReceiptMessage(MessageSource messageSource, OrderReceiptDto payload) : IMessage<OrderReceiptDto>
    {
        public MessageSource MessageSource { get; init; } = messageSource;

        public OrderReceiptDto Payload { get; init; } = payload;
    }
}
