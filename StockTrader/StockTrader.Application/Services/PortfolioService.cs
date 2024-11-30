using Microsoft.EntityFrameworkCore;

using StockTrader.Core.Entities;
using StockTrader.Core.Interfaces;

namespace StockTrader.Application.Services
{
    public class PortfolioService(IRepository<PersonStock> personStockRepository) : IPortfolioService
    {
        public async Task<List<PersonStock>> GetPersonPortfolioAsync(int personId)
            => await personStockRepository
                .GetAll()
                .AsNoTracking()
                .Include(x => x.Stock)
                .Where(x => x.PersonId == personId)
                .ToListAsync();
    }
}
