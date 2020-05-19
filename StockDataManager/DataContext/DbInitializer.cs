namespace StockDataManager
{
    using System.Linq;

    public static class DbInitializer
    {
        public static void Initialize(StockDBContext context)
        {
            // Look for any Stocks
            if (context.Stocks.Any())
            {
                return;   // DB has been seeded
            }

            var stocks = StockLoader.GetStockList();
         
            context.Stocks.AddRange(stocks);

            context.SaveChanges();
        }


    }
}
