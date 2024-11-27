using Testcontainers.PostgreSql;

namespace StockTrader.Infrastructure
{
    public static class DbContainerFactory
    {
        public static PostgreSqlContainer GetPostgreSqlContainer(
            string dbName,
            string dbUser,
            string dbPassword,
            int hostPort,
            int containerPort = 5432)
        {
            var postgreSqlContainer = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithDatabase(dbName)
                .WithUsername(dbUser)
                .WithPassword(dbPassword)
                .WithPortBinding(hostPort, containerPort)
                .Build();

            return postgreSqlContainer;
        }
    }
}
