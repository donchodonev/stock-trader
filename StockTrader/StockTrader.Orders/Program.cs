using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Infrastructure.Data.DbContexts.OrderService;
using StockTrader.Infrastructure.Factories;

var builder = FunctionsApplication.CreateBuilder(args);

var postgreSqlContainer = DbContainerFactory.GetPostgreSqlContainer(builder.Configuration, "OrdersDb", 5551);
await postgreSqlContainer.StartAsync();

builder.Services.AddFunctionsWorkerDefaults();
builder.Services.AddFunctionsWorkerCore();

builder.Services.AddDbContext<OrderServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrdersDb"));
});

var serviceProvider = builder.Services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<OrderServiceDbContext>().Database.Migrate();
}

serviceProvider.GetRequiredService<IHostApplicationLifetime>()
    .ApplicationStopping
    .Register(async () =>
    {
        await postgreSqlContainer.StopAsync();
        await postgreSqlContainer.DisposeAsync();
    });

builder.Build().Run();