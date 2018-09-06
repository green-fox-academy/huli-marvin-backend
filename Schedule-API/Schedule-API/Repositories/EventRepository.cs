using ScheduleAPI.Models;
using ScheduleAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleAPI.Repositories
{
    public class EventRepository : IGenericRepository<Event>
    {
        private EventContext eventContext;
        private PaginationService paginationService;

        public EventRepository(EventContext eventContext, PaginationService paginationService)
        {
            this.eventContext = eventContext;
            this.paginationService = paginationService;
        }

        public void Create(Event occurrence)
        {
            eventContext.Add(occurrence);
            eventContext.SaveChanges();
        }

        public List<Event> Read()
        {
            return eventContext.Events.ToList();
        }

        public void Update(Event occurrence)
        {
            eventContext.Update(occurrence);
            eventContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = eventContext.Events.ToList().FirstOrDefault(x => x.EventId == id);
            eventContext.Remove(removable);
            eventContext.SaveChanges();
        }

        public Event GetItemById(int id)
        {
            return eventContext.Events.ToList().FirstOrDefault(x => x.EventId == id);
        }
        public IEnumerable<Event> GetAll(int pageSize, int pageIndex)
        {
            int itemCount = eventContext.Events.Count();

            if (paginationService.ParameterValidation(pageIndex, pageSize, itemCount))
            {
                return eventContext.Events.Skip(pageIndex * pageSize).Take(paginationService.CalcNumberOfItemsOnPage(
                    pageIndex, pageSize, itemCount));
            }
            return null;
        }
    }
}
