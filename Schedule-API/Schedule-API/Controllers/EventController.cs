using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.ViewModels;

namespace ScheduleAPI.Controllers
{
    public class EventController : Controller
    {
        private EventContext eventContext;
        private EventViewModel eventViewModel;
        private EventRepository eventRepository;
        private EventTemplateRepository eventTemplateRepository;

        public EventController(EventContext eventContext, EventViewModel eventViewModel, EventRepository eventRepository, EventTemplateRepository eventTemplateRepository)
        {
            this.eventContext = eventContext;
            this.eventViewModel = eventViewModel;
            this.eventRepository = eventRepository;
            this.eventTemplateRepository = eventTemplateRepository;
            eventViewModel.Events = eventRepository.GetAll();
            eventViewModel.EventTemplates = eventTemplateRepository.GetAll();
        }

        [HttpGet("api/Events")]
        public IActionResult Index()
        {
            return View("TestView", eventViewModel);
        }

        [HttpGet("api/Events/{id}")]
        public IActionResult Index()
        {
            return View("TestView", eventViewModel);
        }

        [HttpPost("api/Events")]
        public IActionResult Index()
        {
            return View("TestView", eventViewModel);
        }

        [HttpPut("api/Events/{id}")]
        public IActionResult Index()
        {
            return View("TestView", eventViewModel);
        }

        [HttpDelete("api/Events/{id}")]
        public IActionResult Index()
        {
            return View("TestView", eventViewModel);
        }
    }
}
