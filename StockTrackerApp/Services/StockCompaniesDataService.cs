namespace StockTrackerApp.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using StockDataManager;
    using StockTrackerApp.DTO;
    using StockTrackerApp.Interfaces;

    public class StockCompaniesDataService : IStockCompaniesDataService
    {
        private readonly StockDBContext stockDBContext;

        public StockCompaniesDataService(StockDBContext stockDBContext)
        {
            this.stockDBContext = stockDBContext;
        }

        public async Task<StockDto[]> GetStockList()
        {
            return await stockDBContext.Stocks.Select(s => new StockDto(s)).Take(25).ToArrayAsync();
        }
    }
}
