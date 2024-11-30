using Azure.Messaging.ServiceBus;

using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Application.DTOs;
using StockTrader.Application.Handlers;
using StockTrader.Application.Services;
using StockTrader.Core.Interfaces;
using StockTrader.Infrastructure.Clients;
using StockTrader.Infrastructure.Data.DbContexts.PortfolioService;
using StockTrader.Infrastructure.Data.Repositories;
using StockTrader.Infrastructure.Extensions;
using StockTrader.Infrastructure.Factories;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

var postgreSqlContainer = DbContainerFactory.GetPostgreSqlContainer(builder.Configuration, "PortfoliosDb", 5552);
await postgreSqlContainer.StartAsync();

builder.Services.AddFunctionsWorkerDefaults();
builder.Services.AddFunctionsWorkerCore();

builder.Services.AddSingleton(new ServiceBusClient(builder.Configuration.GetConnectionString("AzureServiceBusSendListenConnectionString")));
builder.Services.AddSingleton<IMessageClient, AzureServiceBusClient>();
builder.Services.AddScoped<DbContext, PortfolioServiceDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMessageHandler<IMessage<PriceDto>>, PortfolioPriceMessageHandler>();
builder.Services.AddScoped<IMessageHandler<IMessage<OrderDto>>, OrderMessageHandler>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

builder.Services.AddDbContext<PortfolioServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PortfoliosDb"));
});
builder.Services.MigrateDatabase<PortfolioServiceDbContext>();
builder.Services.RegisterContainerDisposalDelegate(postgreSqlContainer);

builder.Build().Run();
