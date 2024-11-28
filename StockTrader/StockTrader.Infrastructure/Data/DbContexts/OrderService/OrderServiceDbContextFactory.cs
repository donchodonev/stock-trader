using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StockTrader.Infrastructure.Data.DbContexts.OrderService
{
    public class OrderServiceDbContextFactory : IDesignTimeDbContextFactory<OrderServiceDbContext>
    {
        public OrderServiceDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderServiceDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("OrdersDb"));

            return new OrderServiceDbContext(optionsBuilder.Options);
        }
    }
}