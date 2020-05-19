namespace StockDataManager
{
    using System.IO;
    using System.Reflection;
    using System.Text.Json;
    using StockDataManager.Model;

    public class StockLoader
    {
        public static StockCompany[] GetStockList()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var jsonString = File.ReadAllText($@"{directory}\Data\StockData.json");

            StockData stockData = JsonSerializer.Deserialize<StockData>(jsonString);

            return stockData.symbolsList;
        }
    }
}
