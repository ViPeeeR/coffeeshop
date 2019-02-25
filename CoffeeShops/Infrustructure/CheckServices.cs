using CoffeeShops.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeeShops.Infrustructure
{
    public class CheckServices : IHostedService, IDisposable
    {
        private readonly ILogger<CheckServices> _logger;
        private readonly IShopService _shopService;
        private readonly IOrderService _orderService;
        private readonly QueueServices _queueServices;
        private Timer _timer;

        public CheckServices(ILogger<CheckServices> logger,
            IShopService shopService,
            IOrderService orderService,
            QueueServices queueServices)
        {
            _logger = logger;
            _shopService = shopService;
            _orderService = orderService;
            _queueServices = queueServices;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Старт сервиса для проверки связи");
            _timer = new Timer(Tick, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Стоп сервиса для проверки связи");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void Tick(object state)
        {
            while(_queueServices.Shop.Pending)
            {
                if (!await _shopService.Check())
                    break;
                await _queueServices.Shop.Execute();
            }

            while (_queueServices.Order.Pending)
            {
                if (!await _orderService.Check())
                    break;
                await _queueServices.Order.Execute();
            }
        }
    }
}
