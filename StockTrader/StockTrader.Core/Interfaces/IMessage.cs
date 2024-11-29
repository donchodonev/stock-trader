using StockTrader.Core.Enums;

namespace StockTrader.Core.Interfaces
{
    public interface IMessage<T>
    {
        public MessageSource MessageSource { get; init; }

        public T Payload { get; init; }
    }
}
