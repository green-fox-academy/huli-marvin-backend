using Microsoft.AspNetCore.Mvc;
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
        private EventViewModel eventViewModel;
        private EventRepository eventRepository;
        private EventTemplateRepository eventTemplateRepository;
        private PaginationService paginationService;

        private EventController eventController;
        private readonly Event events;

        public EventControllerUnitTest()
        {
            events = new Event();
            eventController = new EventController(eventViewModel, eventRepository, eventTemplateRepository, paginationService);
        }

        [Fact]
        public async Task FindEvent_Should_ReturnEvent_InJson()
        {
            var result = await eventController.GetEvent(events.EventId);
            var jsonResult = result as JsonResult;

            Assert.IsType<JsonResult>(jsonResult);
        }
    }
}
