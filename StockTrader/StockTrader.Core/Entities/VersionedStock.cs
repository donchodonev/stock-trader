namespace StockTrader.Core.Entities
{
    public class VersionedStock : Stock   
    {
        public ICollection<PersonStock> PersonStocks { get; set; }

        public DateTime RowVersion { get; set; }
    }
}
