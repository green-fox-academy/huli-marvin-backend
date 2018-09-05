using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;
using System.Collections.Generic;

namespace ScheduleAPI.Controllers
{
    public class EventController : Controller
    {
        private EventViewModel eventViewModel;
        private EventRepository eventRepository;
        private EventTemplateRepository eventTemplateRepository;
        private PaginationService<Event> paginationService;

        public EventController(EventViewModel eventViewModel, EventRepository eventRepository, EventTemplateRepository eventTemplateRepository, PaginationService<Event> paginationService)
        {
            this.eventViewModel = eventViewModel;
            this.eventRepository = eventRepository;
            this.eventTemplateRepository = eventTemplateRepository;
            this.paginationService = paginationService;
            eventViewModel.Events = eventRepository.GetAll();
            eventViewModel.EventTemplates = eventTemplateRepository.GetAll();
        }

        [HttpGet("api/events")]
        public IActionResult GetAllEvents([FromQuery]int pageSize = 8 , [FromQuery]int pageIndex = 0)
        {
            ICollection<Event> allItems = eventRepository.GetAll();
            return Json(paginationService.GetEventsPagination(allItems, pageSize, pageIndex));
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
