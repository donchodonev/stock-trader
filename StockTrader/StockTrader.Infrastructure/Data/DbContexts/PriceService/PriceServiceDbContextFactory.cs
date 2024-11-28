using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using StockTrader.Infrastructure.Data.DbContexts.PriceService;

public class PriceServiceDbContextFactory : IDesignTimeDbContextFactory<PriceServiceDbContext>
{
    public PriceServiceDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder().Build();

        var optionsBuilder = new DbContextOptionsBuilder<PriceServiceDbContext>();
        optionsBuilder.UseNpgsql(config.GetConnectionString("PriceServiceDb"));

        return new PriceServiceDbContext(optionsBuilder.Options);
    }
}