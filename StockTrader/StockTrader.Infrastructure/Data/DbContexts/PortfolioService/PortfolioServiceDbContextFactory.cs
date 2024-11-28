using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using StockTrader.Infrastructure.Data.DbContexts.PortfolioService;

public class PortfolioServiceDbContextFactory : IDesignTimeDbContextFactory<PortfolioServiceDbContext>
{
    public PortfolioServiceDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder().Build();

        var optionsBuilder = new DbContextOptionsBuilder<PortfolioServiceDbContext>();
        optionsBuilder.UseNpgsql(config.GetConnectionString("PortfoliosDb"));

        return new PortfolioServiceDbContext(optionsBuilder.Options);
    }
}