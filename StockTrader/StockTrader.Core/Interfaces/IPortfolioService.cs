using StockTrader.Core.Entities;

namespace StockTrader.Core.Interfaces
{
    public interface IPortfolioService
    {
        public Task<List<PersonStock>> GetPersonPortfolioAsync(int personId);
    }
}
