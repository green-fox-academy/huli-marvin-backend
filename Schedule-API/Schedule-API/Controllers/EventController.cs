using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;

namespace ScheduleAPI.Controllers
{
    public class EventController : Controller
    {
        private EventContext eventContext;
        private EventViewModel eventViewModel;
        private EventRepository eventRepository;
        private EventTemplateRepository eventTemplateRepository;
        private EventService eventService;

        public EventController(EventContext eventContext, EventViewModel eventViewModel, EventRepository eventRepository, EventTemplateRepository eventTemplateRepository, EventService eventService)
        {
            this.eventContext = eventContext;
            this.eventViewModel = eventViewModel;
            this.eventRepository = eventRepository;
            this.eventTemplateRepository = eventTemplateRepository;
            this.eventService = eventService;
            eventViewModel.Events = eventRepository.GetAll();
            eventViewModel.EventTemplates = eventTemplateRepository.GetAll();
        }

        [HttpGet("api/Events")]
        public IActionResult GetAllEvents()
        {
            try
            {
                if (eventService.GetAllIsValid())
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("api/Events/{id}")]
        public IActionResult GetEvent()
        {
            return View("TestView", eventViewModel);
        }

        [HttpPost("api/Events")]
        public IActionResult PostEvent()
        {
            return View("TestView", eventViewModel);
        }

        [HttpPut("api/Events/{id}")]
        public IActionResult AddEvent()
        {
            return View("TestView", eventViewModel);
        }

        [HttpDelete("api/Events/{id}")]
        public IActionResult DeleteEvent()
        {
            return View("TestView", eventViewModel);
        }
    }
}
