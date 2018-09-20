using Microsoft.AspNetCore.Mvc;
using Moq;
using ScheduleAPI.Controllers;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ScheduleApiUnitTests
{
    public class EventControllerUnitTest
    {
        //private EventViewModel eventViewModel;
        //private EventRepository eventRepository;
        //private EventTemplateRepository eventTemplateRepository;
        //private PaginationService paginationService;

        private EventController eventController;
        private readonly Event events;

        public EventControllerUnitTest()
        {
            events = new Event();
            
        }

        [Fact]
        public async Task FindEvent_Should_ReturnEvent_InJson()
        {
            eventController = new EventController(eventViewModel: null, eventRepository: null, eventTemplateRepository: null, paginationService: null);

            int testSessionId = 1;
            var mockRepo = new Mock<EventRepository>();
            mockRepo.Setup(repo => repo.GetItemByIdAsync(testSessionId))
                .ReturnsAsync((BrainstormSession)null);
            var controller = new SessionController(mockRepo.Object);

            var result = await eventController.GetEvent(events.EventId);
            var jsonResult = result as JsonResult;

            Assert.IsType<JsonResult>(jsonResult);
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 2+2);
        }
    }
}
