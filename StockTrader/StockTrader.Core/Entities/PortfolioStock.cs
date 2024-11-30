namespace StockTrader.Core.Entities
{
    public class PortfolioStock : Stock
    {
        public ICollection<PersonStock> PersonStocks { get; set; }
    }
}
