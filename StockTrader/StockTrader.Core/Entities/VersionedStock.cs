using StockTrader.Core.Interfaces;

namespace StockTrader.Core.Entities
{
    public class VersionedStock : Stock
    {
        public ICollection<PersonStock> PersonStocks { get; set; }
    }
}
