using System.Collections.Generic;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;

namespace ScheduleAPI.Services
{
    public class EventService
    {
        private EventRepository eventRepository;

        public EventService(EventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public bool GetAllIsValid()
        {
            List<Event> eventsFromDb = new List<Event>();
            eventsFromDb = eventRepository.GetAll();
            return eventsFromDb.Count > 0 ? true : false;
        }
    }
}
