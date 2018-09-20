using Microsoft.AspNetCore.Mvc;
using Moq;
using ScheduleAPI.Controllers;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace ScheduleApiUnitTests
{
    public class EventControllerUnitTest
    {
        private readonly Event events;
        private readonly EventController eventController;

        public EventControllerUnitTest()
        {
            var mockService = new Mock<IGenericRepository<Event>>();

            mockService.Setup(srv => srv.GetItemByIdAsync(It.IsAny<int>()))
.Returns(Task.FromResult<Event>(events));

            events = new Event();
            eventController = new EventController(mockService.Object);
        }

        [Fact]
        public async Task GetEvent_Should_ReturnEvent_InJson()
        {
            var result = await eventController.GetEvent(events.EventId);
            var jsonResult = result as JsonResult;

            Assert.IsAssignableFrom<JsonResult>(jsonResult);
        }
    }
}
