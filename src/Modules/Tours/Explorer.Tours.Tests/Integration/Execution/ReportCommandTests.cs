using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Management;
using Explorer.Tours.Core.UseCases.Execution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Execution
{
    [Collection("Sequential")]
    public class ReportCommandTests : BaseToursIntegrationTest
    {
        public ReportCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act

            // Assert - Database
            var storedEntities = dbContext.Reports.Where(r => r.CreatedFor.Month == DateTime.Now.AddMonths(-1).Month && r.CreatedFor.Year == DateTime.Now.AddMonths(-1).Year).ToList();
            storedEntities.ShouldNotBeNull();
            storedEntities.Count.ShouldBe(2);
        }

        private static ReportService CreateReportService(IServiceScope scope)
        {
            return new ReportService(scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>());
        }
    }
}
