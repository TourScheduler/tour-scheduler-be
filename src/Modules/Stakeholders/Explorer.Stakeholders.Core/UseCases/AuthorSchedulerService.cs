using Explorer.Stakeholders.API.Public;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorSchedulerService : BackgroundService
    {
        private readonly IAuthorService _authorService;

        public AuthorSchedulerService(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow.Day == 2)
                {
                    _authorService.Award();
                }

                var now = DateTime.UtcNow;
                var tomorrow = now.AddDays(1).Date;
                var delay = tomorrow - now;

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
