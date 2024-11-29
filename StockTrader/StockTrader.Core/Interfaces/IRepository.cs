using System.Linq.Expressions;

namespace StockTrader.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task SaveChangesAsync();
    }
}
