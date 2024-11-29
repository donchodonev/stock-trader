using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Testcontainers.PostgreSql;

namespace StockTrader.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterContainerDisposalDelegate(this IServiceCollection services, PostgreSqlContainer postgreSqlContainer)
        {
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetRequiredService<IHostApplicationLifetime>()
                .ApplicationStopping
                .Register(async () =>
                {
                    await postgreSqlContainer.StopAsync();
                    await postgreSqlContainer.DisposeAsync();
                });
        }

        public static void MigrateDatabase<T>(this IServiceCollection services) where T : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<T>().Database.Migrate();
            };
        }
    }
}