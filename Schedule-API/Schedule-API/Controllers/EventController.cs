using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;

namespace ScheduleAPI.Controllers
{
    public class EventController : Controller
    {
        private EventViewModel eventViewModel;
        private EventRepository eventRepository;
        private EventTemplateRepository eventTemplateRepository;
        private EventService eventService;

        public EventController(EventViewModel eventViewModel, EventRepository eventRepository, EventTemplateRepository eventTemplateRepository, EventService eventService)
        {
            this.eventViewModel = eventViewModel;
            this.eventRepository = eventRepository;
            this.eventTemplateRepository = eventTemplateRepository;
            this.eventService = eventService;
            eventViewModel.Events = eventRepository.GetAll();
            eventViewModel.EventTemplates = eventTemplateRepository.GetAll();
        }

        [HttpGet("api/events")]
        public IActionResult GetAllEvents()
        {
            return Json(eventRepository.GetAll());
        }

        [HttpGet("api/events/{id}")]
        public IActionResult GetEvent(int id)
        {
            Event xEvent = eventRepository.GetItemById(id);

            if (xEvent == null)
            {
                return NotFound("No item found for your id...");
            }
            else
            {
                return Json(xEvent);
            }
        }

        [HttpPost("api/events")]
        public IActionResult PostEvent([FromBody]Event xEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                eventRepository.Update(xEvent);
                return Created("DB Updated",xEvent);
            }
        }

        [HttpPut("api/events/{id}")]
        public IActionResult AddEvent(int id, [FromBody]Event xEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != xEvent.EventId)
            {
                return BadRequest();
            }

            try
            {
                eventRepository.Update(xEvent);
            }
            catch (System.Exception)
            {
                return NotFound("There is no item with the given Id...");
            }

            return Ok("Product has been updated");
        }

        [HttpDelete("api/events/{id}")]
        public IActionResult DeleteEvent()
        {
            return View("TestView", eventViewModel);
        }
    }
}
