﻿using Microsoft.Extensions.Logging;

using StockTrader.Application.DTOs;
using StockTrader.Application.Factories;
using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Handlers
{
    public class PortfolioPriceMessageHandler(ILoggerFactory loggerFactory, IRepository<PortfolioStock> stockRepository) : IMessageHandler<IMessage<PriceDto>>
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<PortfolioPriceMessageHandler>();

        public async Task HandleAsync(IMessage<PriceDto> message)
        {
            _logger.LogInformation($"{nameof(PortfolioPriceMessageHandler)}.{nameof(HandleAsync)}: {message}");

            var stock = message.ToStock();

            var existingStock = await stockRepository.FirstOrDefaultAsync(x => x.Ticker == stock.Ticker);

            if (existingStock == null)
            {
                await stockRepository.AddAsync(stock);
                return;
            }

            existingStock.Price = stock.Price;
            await stockRepository.SaveChangesAsync();
        }
    }
}
