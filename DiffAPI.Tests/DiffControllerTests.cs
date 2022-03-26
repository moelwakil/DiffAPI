using DiffAPI.Controllers;
using DiffAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DiffAPI.Tests
{
    public class DiffControllerTests
    {
        DiffController _controller;

        public DiffControllerTests()
        {
            var dbContext = GetDatabaseContext();
            _controller = new DiffController(dbContext, new StubBinaryDiffService());
        }

        private DiffContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DiffContext>()
                .UseInMemoryDatabase(databaseName: "DiffAPITest")
                .Options;
            var databaseContext = new DiffContext(options);
            databaseContext.Database.EnsureCreated();
            //databaseContext.DiffObjects.Add(new DiffObject(1, new byte[] {0, 0, 0, 0}, new byte[] {1, 0, 1, 1}));
            databaseContext.DiffObjects.Add(new DiffObject(1, null, null));
            databaseContext.SaveChangesAsync();
            return databaseContext;
        }

        [Fact]
        public async Task AddLeftData_Returns_BadRequest_If_Null_Data()
        {
            var actionResult = await _controller.AddLeftData(1, new DiffContent { Data = null });
            Assert.IsAssignableFrom<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task GetDiffData_Returns_NotFound_With_Missing_Diff_Details()
        {
            var actionResult = await _controller.GetDiffData(1);
            Assert.IsAssignableFrom<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task GetDiffData_Returns_Ok_With_Diff_Details()
        {
            await _controller.AddLeftData(1, new DiffContent { Data = new byte[] { 0, 0, 0, 0 } });
            await _controller.AddRightData(1, new DiffContent { Data = new byte[] { 1, 0, 1, 0 } });
            var actionResult = await _controller.GetDiffData(1);
            Assert.IsAssignableFrom<OkObjectResult>(actionResult);
        }
    }
}
