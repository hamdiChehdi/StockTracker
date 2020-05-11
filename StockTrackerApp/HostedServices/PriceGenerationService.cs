namespace StockTrackerApp.Services
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using StockDataManager;
    using StockDataManager.Model;
    using StockTrackerApp.DTO;
    using StockTrackerApp.Hub;

    public class PriceGenerationService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PriceGenerationService> _logger;

        private IHubContext<PriceHub> _hub;
        private StockCompany[] stocks;
        private Timer _timer;
        private bool isDisposed;

        public PriceGenerationService(IServiceProvider serviceProvider,
            ILogger<PriceGenerationService> logger,
            IHubContext<PriceHub> hub)
        {
            _hub = hub;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        private LastPriceDto GenerateRandomPrice()
        {
            Random random = new Random();

            int index = random.Next(stocks.Length);
            int minPrice = (int)(stocks[index].price * 0.5m);
            int maxPrice = (int)(stocks[index].price * 1.8m);

            var lastPriceDto = new LastPriceDto();
            lastPriceDto.symbol = stocks[index].symbol;
            lastPriceDto.price = random.Next(minPrice, maxPrice) / 1.1m;

            return lastPriceDto;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            LoadStocks();

            _timer = new Timer(GenerateAndSendPrice, null, 1000, 2000);

            return Task.CompletedTask;
        }

        private void GenerateAndSendPrice(object state)
        {
            var lastPrice = GenerateRandomPrice();
            _hub.Clients.All.SendAsync("PriceUpdated", lastPrice);
        }

        private void LoadStocks()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Get the DbContext instance
                var myDbContext = scope.ServiceProvider.GetRequiredService<StockDBContext>();

                stocks = myDbContext.Stocks.Take(25).ToArray();
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Price generation Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                _timer?.Dispose();
            }

            isDisposed = true;
        }

        ~PriceGenerationService()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}
