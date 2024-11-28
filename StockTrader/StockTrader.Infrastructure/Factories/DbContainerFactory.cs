using DotNet.Testcontainers.Builders;

using Microsoft.Extensions.Configuration;

using StockTrader.Infrastructure.Extensions;

using Testcontainers.PostgreSql;

namespace StockTrader.Infrastructure.Factories
{
    public static class DbContainerFactory
    {
        public static PostgreSqlContainer GetPostgreSqlContainer(
            IConfiguration configuration,
            string databaseConfigKey,
            int hostPort,
            int containerPort = 5432)
        {
            configuration
                .GetConnectionString(databaseConfigKey)
                .GetConnectionData(out var dbName, out var dbUser, out var dbPassword);

            var containerLogEnsuringPgIsUp = "database system is ready to accept connections";
            var postgreSqlContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase(dbName)
                .WithUsername(dbUser)
                .WithPassword(dbPassword)
                .WithPortBinding(hostPort, containerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(containerLogEnsuringPgIsUp))

                .Build();

            return postgreSqlContainer;
        }
    }
}
