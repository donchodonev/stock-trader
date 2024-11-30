namespace StockTrader.Application.Constants
{
    public static class MessagingConstants
    {
        public static class Topics
        {
            public const string PRICES = "prices";
            public const string ORDER_SENT = "ordersent";
            public const string ORDER_RETURNED = "orderreturned";
        }

        public static class Subscriptions
        {
            public const string ORDER = "order";
            public const string PORTFOLIO = "portfolio";
        }
    }
}
