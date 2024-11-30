using Microsoft.EntityFrameworkCore;

using StockTrader.Core.Entities;

namespace StockTrader.Infrastructure.Data.DbContexts.OrderService
{
    public class OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Order { get; set; }

        public DbSet<Stock> Stock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Order

            modelBuilder
                .Entity<Order>()
                .Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn();

            modelBuilder
                .Entity<Order>()
                .Property(x => x.PersonId)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .Property(x => x.Ticker)
                .IsRequired()
                .HasMaxLength(4);

            modelBuilder
                .Entity<Order>()
                .Property(x => x.Quantity)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .Property(x => x.OrderStatus)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .Property(x => x.OrderAction)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .Property(x => x.CreatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .Property(x => x.UpdatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Order>()
                .HasKey(x => x.Id);

            #endregion

            #region Stock

            modelBuilder
               .Entity<Stock>()
               .Property(p => p.Id)
               .IsRequired()
               .UseIdentityColumn();

            modelBuilder
                .Entity<Stock>()
                .Property(p => p.Ticker)
                .HasMaxLength(4)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .Property(p => p.CreatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .Property(p => p.UpdatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .HasKey(p => p.Id);

            #endregion

        }
    }
}