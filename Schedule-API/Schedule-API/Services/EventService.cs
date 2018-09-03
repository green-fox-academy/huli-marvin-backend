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
            List<Event> allItems = eventRepository.GetAll();

            if (ParameterValidation(pageIndex, pageSize, allItems.Count))
            {
                for (int i = pageIndex * pageSize; i < CalcLastItemOfPage(pageIndex, pageSize, allItems.Count); i++)
                {
                    result.Add(allItems[i]);
                }
            }
            return result;
        }

        private bool ParameterValidation(int pageIndex, int pageSize, int itemCount)
        {
            if (pageSize * (pageIndex) < itemCount)
            {
                return true;
            }
            return false;
        }

        private int CalcLastItemOfPage(int pageIndex, int pageSize, int count)
        {
            if ((pageIndex + 1) * pageSize < count)
            {
                return (pageIndex + 1) * pageSize;
            }
            return count;
        }
    }
}
