using Azure.Messaging.ServiceBus;

using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Core.Interfaces;
using StockTrader.Infrastructure.Clients;
using StockTrader.Infrastructure.Data.DbContexts.OrderService;
using StockTrader.Infrastructure.Extensions;
using StockTrader.Infrastructure.Factories;

var builder = FunctionsApplication.CreateBuilder(args);

var postgreSqlContainer = DbContainerFactory.GetPostgreSqlContainer(builder.Configuration, "OrdersDb", 5551);
await postgreSqlContainer.StartAsync();

builder.Services.AddFunctionsWorkerDefaults();
builder.Services.AddFunctionsWorkerCore();
builder.Services.AddSingleton(new ServiceBusClient(builder.Configuration.GetConnectionString("AzureServiceBusSendListenConnectionString")));
builder.Services.AddSingleton<IMessageClient, AzureServiceBusClient>();

builder.Services.AddDbContext<OrderServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrdersDb"));
});
builder.Services.MigrateDatabase<OrderServiceDbContext>();
builder.Services.RegisterContainerDisposalDelegate(postgreSqlContainer);

builder.Build().Run();