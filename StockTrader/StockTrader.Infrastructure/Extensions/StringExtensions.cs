namespace StockTrader.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static void GetConnectionData(this string connectionString, out string dbName, out string user, out string password)
        {
            var res = connectionString
                .Split(';')
                .ToDictionary(x => x.Split('=')[0], y => y.Split('=')[1]);

            dbName = res["Database"];
            user = res["Username"];
            password = res["Password"];
        }
    }
}
