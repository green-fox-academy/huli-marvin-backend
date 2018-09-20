using Microsoft.AspNetCore.Mvc;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;
using System.Threading.Tasks;

namespace ScheduleAPI.Controllers
{
    public class EventController : Controller
    {
        private EventViewModel eventViewModel;
        private EventRepository eventRepository;
        private EventTemplateRepository eventTemplateRepository;
        private PaginationService paginationService;

        public EventController(EventViewModel eventViewModel, EventRepository eventRepository, EventTemplateRepository eventTemplateRepository, PaginationService paginationService)
        {
            this.eventViewModel = eventViewModel;
            this.eventRepository = eventRepository;
            this.eventTemplateRepository = eventTemplateRepository;
            this.paginationService = paginationService;
        }

        [HttpGet("api/events")]
        public IActionResult GetAllEvents([FromQuery]int pageSize = 8 , [FromQuery]int pageIndex = 0)
        {
            return Json(eventRepository.GetAll(pageSize, pageIndex));
        }

        [HttpGet("api/events/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            Event occurrence = await eventRepository.GetItemByIdAsync(id);

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
        public async Task<IActionResult> PostEvent([FromBody]Event occurrence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await eventRepository.UpdateAsync(occurrence);
                return Created("DB Updated", occurrence);
            }
        }

        [HttpPut("api/events/{id}")]
        public async Task<IActionResult> AddEvent(int id, [FromBody]Event occurrence)
        {
            if (id != occurrence.EventId)
            {
                return BadRequest();
            }

            try
            {
                await eventRepository.UpdateAsync(occurrence);
            }
            catch (System.Exception)
            {
                return NotFound("There is no event with the given Id...");
            }

            return Ok("Event has been updated");
        }

        [HttpDelete("api/events/{id}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            Event occurrence = await eventRepository.GetItemByIdAsync(id);

            if (occurrence == null)
            {
                return NotFound("No event found for your id...");
            }
            else
            {
                await eventRepository.DeleteAsync(id);
                return Ok("Event deleted...");
            }
        }
    }
}
