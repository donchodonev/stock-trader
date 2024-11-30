using Microsoft.EntityFrameworkCore;

using StockTrader.Application.Constants;
using StockTrader.Application.DTOs;
using StockTrader.Application.Factories;
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
                await messageClient.SendMessageAsync(message.ToFailedOrderMessage(), MessagingConstants.Topics.ORDER_RETURNED);
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
                    await messageClient.SendMessageAsync(message.ToFailedOrderMessage(), MessagingConstants.Topics.ORDER_RETURNED);
                    return;
                }

                person = new Person { Id = message.Payload.PersonId, };
                person.PersonStocks = new List<PersonStock> { personNewStock };
                await personRepository.AddAsync(person);
                return;
            }

            var personExistingStock = person.PersonStocks.FirstOrDefault(x => x.StockId == stock.Id);

            if (personExistingStock == null)
            {
                if (isSellOperation)
                {
                    await messageClient.SendMessageAsync(message.ToFailedOrderMessage(), MessagingConstants.Topics.ORDER_RETURNED);
                    return;
                }

                person.PersonStocks.Add(personNewStock);
            }
            else
            {
                if (isSellOperation && personExistingStock.Quantity - message.Payload.Quantity < 0)
                {
                    await messageClient.SendMessageAsync(message.ToFailedOrderMessage(), MessagingConstants.Topics.ORDER_RETURNED);
                    return;
                }

                personExistingStock.Quantity += message.Payload.Quantity;
            }

            await personRepository.SaveChangesAsync();
        }
    }
}
