using Explorer.Tours.API.Public.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class ReportSchedulerService : BackgroundService
    {
        private readonly IReportService _reportService;

        public ReportSchedulerService(IReportService reportService)
        {
            _reportService = reportService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow.Day == 25) // izmeniti kasnije
                {
                    _reportService.Create();
                }

                var now = DateTime.UtcNow;
                var tomorrow = now.AddDays(1).Date;
                var delay = tomorrow - now;

                await Task.Delay(delay, stoppingToken);
            }
        }

    }
}
