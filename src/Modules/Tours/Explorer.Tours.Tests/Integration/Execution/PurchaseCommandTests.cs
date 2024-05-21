using Explorer.API.Controllers.Author.Tours;
using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Tours.API.Dtos;
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
            var newEntity = new CreatePurchaseDto
            {
                TourId = -1,
                TouristId = -21
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CreatePurchaseDto;

            // Assert - Response
            result.ShouldNotBeNull();
            /*result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);*/

            /*// Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);*/
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newEntity = new CreatePurchaseDto
            {
                TourId = -100
            };

            // Act
            var result = (ObjectResult)controller.Create(newEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static PurchaseController CreateController(IServiceScope scope)
        {
            return new PurchaseController()
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
