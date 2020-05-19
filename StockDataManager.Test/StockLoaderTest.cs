using System;
using Xunit;

namespace StockDataManager.Test
{
    public class StockLoaderTest
    {
        [Fact]
        public void Test_GetStockList()
        {
            var result = StockLoader.GetStockList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(14048, result.Length);
        }
    }
}
