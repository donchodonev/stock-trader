using StockTrader.Core.Interfaces;

namespace StockTrader.Core.Entities
{
    public class Person: IAuditable
    {
        public int Id { get; set; }

        public ICollection<PersonStock> PersonStocks { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
