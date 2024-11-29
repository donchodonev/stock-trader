namespace StockTrader.Core.Interfaces
{
    public interface IMessageClient
    {
        Task SendMessageAsync<T>(IMessage<T> message, string topicName);
    }
}
