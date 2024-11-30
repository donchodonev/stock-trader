using Microsoft.EntityFrameworkCore;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Messages;
using StockTrader.Core.Entities;
using StockTrader.Core.Enums;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Handlers
{
    public class OrderMessageHandler(
        IRepository<Person> personRepository,
        IRepository<PortfolioStock> portfolioStock,
        IMessageClient messageClient) : IMessageHandler<IMessage<OrderDto>>
    {
        public async Task HandleAsync(IMessage<OrderDto> message)
        {
            var stock = await portfolioStock.FirstOrDefaultAsync(x => x.Ticker == message.Payload.Ticker);

            if (stock == null)
            {
                await SendStockNotFoundFailureMessageAsync(message.Payload.Id);
                return;
            }

            var person = await personRepository
                .GetAll()
                .Where(x => x.Id == message.Payload.PersonId)
                .Include(x => x.PersonStocks)
                .FirstOrDefaultAsync();

            var personNewStock = new PersonStock { Quantity = message.Payload.Quantity, Stock = stock };

            var isSellOperation = message.Payload.OrderAction == OrderAction.Sell;
            if (person == null)
            {
                if (isSellOperation)
                {
                    await SendPersonCannotSellOnFirstOrderMessageAsync(message.Payload.Id);
                    return;
                }

                person = new Person { Id = message.Payload.PersonId, };
                person.PersonStocks = new List<PersonStock> { personNewStock };
                await personRepository.AddAsync(person);
                await SendSuccessMessage(message.Payload.Id);
                return;
            }

            var personExistingStock = person.PersonStocks.FirstOrDefault(x => x.StockId == stock.Id);

            if (personExistingStock == null)
            {
                if (isSellOperation)
                {
                    await SendPersonCannotHaveNegativeQuantityOfStockMessageAsync(message.Payload.Id);
                    return;
                }

                person.PersonStocks.Add(personNewStock);
            }
            else
            {
                if (isSellOperation && personExistingStock.Quantity - message.Payload.Quantity < 0)
                {
                    await SendPersonCannotHaveNegativeQuantityOfStockMessageAsync(message.Payload.Id);
                    return;
                }

                personExistingStock.Quantity += message.Payload.Quantity;
            }

            await SendSuccessMessage(message.Payload.Id);
            await personRepository.SaveChangesAsync();
        }

        private Task SendStockNotFoundFailureMessageAsync(long messageId)
            => messageClient.SendMessageAsync(
                new OrderReceiptMessage(
                    MessageSource.PortfolioService,
                    new OrderReceiptDto(messageId, OrderStatus.Failed, "Stock not found")), MessagingConstants.Topics.ORDER_RETURNED);

        private Task SendPersonCannotSellOnFirstOrderMessageAsync(long messageId)
            => messageClient.SendMessageAsync(
                new OrderReceiptMessage(
                    MessageSource.PortfolioService,
                    new OrderReceiptDto(messageId, OrderStatus.Failed, "Person's first order cannot be a selling operation")), MessagingConstants.Topics.ORDER_RETURNED);

        private Task SendPersonCannotHaveNegativeQuantityOfStockMessageAsync(long messageId)
            => messageClient.SendMessageAsync(
                new OrderReceiptMessage(
                    MessageSource.PortfolioService,
                    new OrderReceiptDto(messageId, OrderStatus.Failed, "Person cannot sell stock he has no quantity of")), MessagingConstants.Topics.ORDER_RETURNED);

        private Task SendSuccessMessage(long messageId)
            => messageClient.SendMessageAsync(
                new OrderReceiptMessage(
                    MessageSource.PortfolioService,
                    new OrderReceiptDto(messageId, OrderStatus.Completed, "Order processed, portfolio updated")), MessagingConstants.Topics.ORDER_RETURNED);
    }
}
