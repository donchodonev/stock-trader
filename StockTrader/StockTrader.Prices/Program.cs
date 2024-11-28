using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Infrastructure.Data.DbContexts.PriceService;
using StockTrader.Infrastructure.Factories;

var builder = FunctionsApplication.CreateBuilder(args);

var postgreSqlWriteContainer = DbContainerFactory.GetPostgreSqlContainer(builder.Configuration, "PriceDb", 5554);
await postgreSqlWriteContainer.StartAsync();

builder.Services.AddFunctionsWorkerDefaults();
builder.Services.AddFunctionsWorkerCore();

builder.Services.AddDbContext<PriceServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PriceDb"));
});

var serviceProvider = builder.Services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<PriceServiceDbContext>().Database.Migrate();
}

serviceProvider.GetRequiredService<IHostApplicationLifetime>()
    .ApplicationStopping
    .Register(async () =>
    {
        await postgreSqlWriteContainer.StopAsync();
        await postgreSqlWriteContainer.DisposeAsync();
    });

builder.Build().Run();