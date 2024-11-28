using Microsoft.EntityFrameworkCore;

using StockTrader.Core.Entities;

namespace StockTrader.Infrastructure.Data.DbContexts.PriceService
{
    public class PriceServiceDbContext(DbContextOptions<PriceServiceDbContext> options) : DbContext(options)
    {
        public DbSet<Stock> Stock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Stock>()
                .Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn();

            modelBuilder
                .Entity<Stock>()
                .Property(x => x.Ticker)
                .IsRequired()
                .HasMaxLength(4);

            modelBuilder
                .Entity<Stock>()
                .Property(x => x.CreatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .Property(x => x.UpdatedOn)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .Property(x => x.Price)
                .IsRequired();

            modelBuilder
                .Entity<Stock>()
                .HasKey(x => x.Id);
        }
    }
}