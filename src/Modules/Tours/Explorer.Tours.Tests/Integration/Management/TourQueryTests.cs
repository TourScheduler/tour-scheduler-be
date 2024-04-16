using Explorer.API.Controllers.Author.Tours;
using Explorer.BuildingBlocks.Core.UseCases;
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

namespace Explorer.Tours.Tests.Integration.Management
{
    [Collection("Sequential")]
    public class TourQueryTests : BaseToursIntegrationTest
    {
        public TourQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all_by_author_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetByAuthorId(150).Result)?.Value as List<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public void Filters_all_by_status_draft()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.FilterByStatus(150, 0).Result)?.Value as List<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public void Filters_all_by_status_published()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.FilterByStatus(150, 1).Result)?.Value as List<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void Filters_all_by_status_archived()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.FilterByStatus(150, 2).Result)?.Value as List<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
