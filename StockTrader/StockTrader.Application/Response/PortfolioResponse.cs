using StockTrader.Application.DTOs;

namespace StockTrader.Application.Response
{
    public class PortfolioResponse
    {
        public List<StockDto> Stocks { get; set; }

        public decimal TotalValue => Stocks?.Sum(x => x.Price * x.Quantity) ?? 0M;
    }
}
