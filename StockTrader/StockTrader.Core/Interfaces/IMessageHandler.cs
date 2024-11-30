namespace StockTrader.Core.Interfaces
{
    public interface IMessageHandler<TMessage>
    {
        Task HandleAsync(TMessage message);
    }
}
