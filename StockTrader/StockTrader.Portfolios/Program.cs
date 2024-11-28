using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Infrastructure.Factories;

var builder = FunctionsApplication.CreateBuilder(args);
var postgreSqlContainer = DbContainerFactory.GetPostgreSqlContainer(builder.Configuration, "PortfoliosDb", 5552);
await postgreSqlContainer.StartAsync();

var serviceProvider = builder
    .Services
    .BuildServiceProvider()
    .GetRequiredService<IHostApplicationLifetime>()
    .ApplicationStopping
    .Register(async () =>
    {
        await postgreSqlContainer.StopAsync();
        await postgreSqlContainer.DisposeAsync();
        Console.WriteLine("Container disposed");
    });

builder.Services.AddFunctionsWorkerDefaults();
builder.Services.AddFunctionsWorkerCore();
builder.Build().Run();
