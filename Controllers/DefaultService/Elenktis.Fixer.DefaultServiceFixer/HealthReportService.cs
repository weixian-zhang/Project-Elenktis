using System;
using System.Threading;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Fixer.DefaultServiceFixer
{
    public class HealthReportService : BackgroundService
    {
        public HealthReportService(IEndpointInstance bus)
        {
            _bus = bus;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new Timer
                (ReportHealth, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private void ReportHealth(object state)
        {
            _bus.Send(new HealthEvent()
            {
                Controller = ControllerUri.DefaultServiceSpy
            }).GetAwaiter().GetResult();
        }

        private Timer _timer;

        private IEndpointInstance _bus;
    }
}