namespace StockDataManager.Model
{
    public class StockCompany
    {
        public long Id { get; set; }

        public string symbol { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }

        public string exchange { get; set; }

        public decimal latestPrice { get; set; }
    }
}
