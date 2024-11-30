using Microsoft.EntityFrameworkCore;

using StockTrader.Core.Interfaces;

using System.Linq.Expressions;

namespace StockTrader.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.FirstOrDefaultAsync(predicate);

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditable)
                {
                    if (entry.State == EntityState.Modified)
                    {
                        ((IAuditable)entry.Entity).UpdatedOn = now;
                    }
                    else if (entry.State == EntityState.Added)
                    {
                        ((IAuditable)entry.Entity).CreatedOn = now;
                        ((IAuditable)entry.Entity).UpdatedOn = now;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
