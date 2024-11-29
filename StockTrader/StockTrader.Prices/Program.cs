using Azure.Messaging.ServiceBus;

using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Application.Services;
using StockTrader.Core.Interfaces;
using StockTrader.Infrastructure.Clients;
using StockTrader.Infrastructure.Data.DbContexts.PriceService;
using StockTrader.Infrastructure.Data.Repositories;
using StockTrader.Infrastructure.Extensions;
using StockTrader.Infrastructure.Factories;

var builder = FunctionsApplication.CreateBuilder(args);
var postgreSqlWriteContainer = DbContainerFactory.GetPostgreSqlContainer(builder.Configuration, "PriceDb", 5554);
await postgreSqlWriteContainer.StartAsync();

builder.Services.AddFunctionsWorkerDefaults();
builder.Services.AddFunctionsWorkerCore();
builder.Services.AddSingleton(new ServiceBusClient(builder.Configuration.GetConnectionString("AzureServiceBusSendOnlyConnectionString")));
builder.Services.AddSingleton<IMessageClient, AzureServiceBusClient>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<DbContext, PriceServiceDbContext>();
builder.Services.AddDbContext<PriceServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PriceDb"));
});

builder.Services.MigrateDatabase<PriceServiceDbContext>();
builder.Services.RegisterContainerDisposalDelegate(postgreSqlWriteContainer);

builder.Build().Run();