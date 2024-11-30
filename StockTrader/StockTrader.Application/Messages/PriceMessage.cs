using StockTrader.Application.DTOs;
using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Messages
{
    public class PriceMessage(MessageSource messageSource, PriceDto payload) : IMessage<PriceDto>
    {
        public MessageSource MessageSource { get; init; } = messageSource;

        public PriceDto Payload { get; init; } = payload;
    }
}
