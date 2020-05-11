namespace StockTrackerApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using StockTrackerApp.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class StockCompaniesController : ControllerBase
    {
        private readonly IStockCompaniesDataService stockCompaniesDataService;

        public StockCompaniesController(IStockCompaniesDataService stockCompaniesDataService)
        {
            this.stockCompaniesDataService = stockCompaniesDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await stockCompaniesDataService.GetStockList());
        }
    }
}