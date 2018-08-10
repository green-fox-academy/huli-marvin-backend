using ScheduleAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Services
{
    public class EventService
    {
        private EventRepository eventRepository;

        public EventService(EventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }
    }
}
