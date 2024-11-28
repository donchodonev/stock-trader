using StockTrader.Core.Interfaces;

namespace StockTrader.Core.Entities
{
    public class PersonStock: IAuditable
    {
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int StockId { get; set; }

        public VersionedStock Stock { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
