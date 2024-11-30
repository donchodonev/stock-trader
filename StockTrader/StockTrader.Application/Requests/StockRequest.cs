using StockTrader.Core.Enums;
using StockTrader.Core.Requests;

using System.Text.Json.Serialization;

namespace StockTrader.Application.Requests
{
    public class StockRequest : IStockRequest
    {
        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("orderAction")]
        public OrderAction OrderAction { get; set; }
    }
}
