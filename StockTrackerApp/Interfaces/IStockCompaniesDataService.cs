using StockTrackerApp.DTO;
using System.Threading.Tasks;

namespace StockTrackerApp.Interfaces
{
    public interface IStockCompaniesDataService
    {
        Task<StockDto[]> GetStockList();
    }
}