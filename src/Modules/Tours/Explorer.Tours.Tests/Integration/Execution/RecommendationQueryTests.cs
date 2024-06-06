using Explorer.API.Controllers.Author.Tours;
using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Management;
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
    public class RecommendationQueryTests : BaseToursIntegrationTest
    {
        public RecommendationQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Filters_all_by_status_awarded()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.FilterByAwardStatus(true).Result)?.Value as List<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void Filters_all_by_status_unawarded()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.FilterByAwardStatus(false).Result)?.Value as List<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        private static RecommendationController CreateController(IServiceScope scope)
        {
            return new RecommendationController(scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<Stakeholders.API.Public.IAuthorService>(), scope.ServiceProvider.GetRequiredService<Stakeholders.API.Public.ITouristService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
