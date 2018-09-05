using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Event> GetEventsPagination(int pageSize, int pageIndex)
        {
            IEnumerable<Event> result = new List<Event>();
            ICollection<Event> allItems = eventRepository.GetAll();

            if (ParameterValidation(pageIndex, pageSize, allItems.Count))
            {

                return allItems.Skip(pageIndex * pageSize).Take(CalcNumberOfItemsOnPage(pageIndex, pageSize, allItems.Count));
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

        private int CalcNumberOfItemsOnPage(int pageIndex, int pageSize, int itemCount)
        {
            if ((pageIndex + 1) * pageSize > itemCount)
            {
                return itemCount - (pageSize * (pageIndex));
            }
            return pageSize;
        }
    }
}
