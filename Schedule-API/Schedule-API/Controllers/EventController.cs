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
            Event occurrence = eventRepository.GetItemById(id);

            if (occurrence == null)
            {
                return NotFound("No item found for your id...");
            }
            else
            {
                return Json(occurrence);
            }
        }

        [HttpPost("api/events")]
        public IActionResult PostEvent([FromBody]Event occurrence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                eventRepository.Update(occurrence);
                return Created("DB Updated", occurrence);
            }
        }

        [HttpPut("api/events/{id}")]
        public IActionResult AddEvent(int id, [FromBody]Event occurrence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != occurrence.EventId)
            {
                return BadRequest();
            }

            try
            {
                eventRepository.Update(occurrence);
            }
            catch (System.Exception)
            {
                return NotFound("There is no event with the given Id...");
            }

            return Ok("Event has been updated");
        }

        [HttpDelete("api/events/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            Event occurrence = eventRepository.GetItemById(id);

            if (occurrence == null)
            {
                return NotFound("No event found for your id...");
            }
            else
            {
            eventRepository.Delete(id);
            return Ok("Event deleted...");
            }
        }
    }
}
