namespace StockTrackerApp.DTO
{
    using StockDataManager.Model;

    public class StockDto
    {

        public StockDto(StockCompany s)
        {
            symbol = s.symbol;
            name = s.name;
            price = s.price;
            exchange = s.exchange;
        }

        public string symbol { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }

        public string exchange { get; set; }
    }
}
