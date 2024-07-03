using Explorer.API.Controllers.Author.Tours;
using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Management;
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
    public class PurchaseCommandTests : BaseToursIntegrationTest
    {
        public PurchaseCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newList = new List<CreatePurchaseDto>();
            var newEntity = new CreatePurchaseDto
            {
                TourId = -1,
                TouristId = -21
            };
            newList.Add(newEntity);

            // Act
            var result = ((ObjectResult)controller.Create("cedomirstevanovic8@gmail.com", newList).Result)?.Value as List<PurchaseDto>;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Count.ShouldNotBe(0);

            // Assert - Database
            var storedEntity = dbContext.Purchases.FirstOrDefault(p => p.Id == result[0].Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result[0].Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newList = new List<CreatePurchaseDto>();
            var newEntity = new CreatePurchaseDto
            {
                TourId = -100
            };
            newList.Add(newEntity);

            // Act
            var result = (ObjectResult)controller.Create("cedomirstevanovic8@gmail.com", newList).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static PurchaseController CreateController(IServiceScope scope)
        {
            return new PurchaseController(scope.ServiceProvider.GetRequiredService<IPurchaseService>(), scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
