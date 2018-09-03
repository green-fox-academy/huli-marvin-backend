using System;
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

        public List<Event> GetEventsPagination(int pageSize, int pageIndex)
        {
            List<Event> result = new List<Event>();
            List<Event> allEvents = eventRepository.GetAll();

            if (ParameterValidation(pageIndex, pageSize, allEvents.Count))
            {
                for (int i = pageIndex * pageSize; i < CalcLastEventOfPage(pageIndex, pageSize, allEvents.Count); i++)
                {
                    result.Add(allEvents[i]);
                }
            }
            return result;
        }

        private bool ParameterValidation(int pageIndex, int pageSize, int eventCount)
        {
            if (pageSize * (pageIndex) < eventCount)
            {
                return true;
            }
            return false;
        }

        private int CalcLastEventOfPage(int pageIndex, int pageSize, int count)
        {
            if ((pageIndex + 1) * pageSize < count)
            {
                return (pageIndex + 1) * pageSize;
            }
            return count;
        }
    }
}
