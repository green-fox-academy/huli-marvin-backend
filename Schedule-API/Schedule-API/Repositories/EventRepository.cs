using ScheduleAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleAPI.Repositories
{
    public class EventRepository : IGenericRepository<Event>
    {
        private EventContext eventContext;

        public EventRepository(EventContext eventContext)
        {
            this.eventContext = eventContext;
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
            var removable = eventContext.Events.ToList().FirstOrDefault(x => x.Id == id);
            eventContext.Remove(removable);
            eventContext.SaveChanges();
        }

        public Event GetItemById(int id)
        {
            return eventContext.Events.ToList().FirstOrDefault(x => x.Id == id);
        }

        public List<Event> GetAll()
        {
            return eventContext.Events.ToList();
        }
    }
}
