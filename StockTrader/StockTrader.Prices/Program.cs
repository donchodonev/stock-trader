using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StockTrader.Utils;

var postgreSqlContainer = DbContainerFactory.GetPostgreSqlContainer("Test3", "user", "password", 5434);

await postgreSqlContainer.StartAsync();

var builder = FunctionsApplication.CreateBuilder(args);

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