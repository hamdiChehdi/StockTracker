using Microsoft.EntityFrameworkCore;
using StockDataManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockDataManager
{
    public class StockDBContext : DbContext
    {
        public StockDBContext(DbContextOptions<StockDBContext> options)
            : base(options)
        {
        }

        public DbSet<StockCompany> Stocks { get; set; }
    }
}
