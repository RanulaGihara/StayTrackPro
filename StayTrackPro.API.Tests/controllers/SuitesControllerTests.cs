using Microsoft.EntityFrameworkCore;
using StayTrackPro.API.Controllers;
using StayTrackPro.API.DTOs;
using StayTrackPro.API.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StayTrackPro.API.Tests.Controllers
{
    public class SuitesControllerTests
    {
        private StayTrackProDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<StayTrackProDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // unique per test
                .Options;

            var context = new StayTrackProDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetSuites_ReturnsEmptyList_WhenNoSuitesExist()
        {
            var context = GetInMemoryDbContext();
            var controller = new SuitesController(context);

            var result = await controller.GetSuites();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var suites = Assert.IsAssignableFrom<IEnumerable<SuiteDto>>(okResult.Value);
            Assert.Empty(suites);
        }

        [Fact]
        public async Task CreateSuite_AddsSuiteSuccessfully()
        {
            var context = GetInMemoryDbContext();
            var controller = new SuitesController(context);

            var newSuite = new SuiteDto
            {
                SuiteName = "Deluxe Room",
                Type = "Luxury",
                PricePerNight = 199.99m
            };

            var result = await controller.CreateSuite(newSuite);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            var returnedSuite = Assert.IsType<SuiteDto>(createdAt.Value);

            Assert.Equal("Deluxe Room", returnedSuite.SuiteName);
            Assert.Equal("Luxury", returnedSuite.Type);
            Assert.Equal(199.99m, returnedSuite.PricePerNight);
        }

        [Fact]
        public async Task GetSuite_ReturnsNotFound_WhenSuiteDoesNotExist()
        {
            var context = GetInMemoryDbContext();
            var controller = new SuitesController(context);

            var result = await controller.GetSuite(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteSuite_RemovesSuite_WhenExists()
        {
            var context = GetInMemoryDbContext();
            context.Suites.Add(new Suite
            {
                SuiteName = "Test Suite",
                Type = "Economy",
                PricePerNight = 50
            });
            await context.SaveChangesAsync();

            var controller = new SuitesController(context);
            var suiteId = 1;

            var result = await controller.DeleteSuite(suiteId);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Suites);
        }

        [Fact]
        public async Task UpdateSuite_UpdatesFields_WhenSuiteExists()
        {
            var context = GetInMemoryDbContext();
            var suite = new Suite
            {
                SuiteName = "Old Name",
                Type = "Economy",
                PricePerNight = 100
            };
            context.Suites.Add(suite);
            await context.SaveChangesAsync();

            var controller = new SuitesController(context);
            var updatedDto = new SuiteDto
            {
                SuiteName = "Updated Name",
                Type = "Luxury",
                PricePerNight = 250
            };

            var result = await controller.UpdateSuite(suite.Id, updatedDto);

            Assert.IsType<NoContentResult>(result);

            var updatedSuite = await context.Suites.FindAsync(suite.Id);
            Assert.Equal("Updated Name", updatedSuite.SuiteName);
            Assert.Equal("Luxury", updatedSuite.Type);
            Assert.Equal(250, updatedSuite.PricePerNight);
        }
    }
}
