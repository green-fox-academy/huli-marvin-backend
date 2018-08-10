using ScheduleAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleAPI.Repositories
{
    public class EventRepository : IGenericRepository<Event>
    {
        private EventContext eventContextObj;

        public EventRepository(EventContext eventContextObj)
        {
            this.eventContextObj = eventContextObj;
        }

        public void Create(Event occurrence)
        {
            eventContextObj.Add(occurrence);
            eventContextObj.SaveChanges();
        }

        public List<Event> Read()
        {
            return eventContextObj.Events.ToList();
        }

        public void Update(Event occurrence)
        {
            eventContextObj.Update(occurrence);
            eventContextObj.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = eventContextObj.Events.ToList().FirstOrDefault(x => x.Id == id);
            eventContextObj.Remove(removable);
            eventContextObj.SaveChanges();
        }

        public Event GetItemById(int id)
        {
            return eventContextObj.Events.ToList().FirstOrDefault(x => x.Id == id);
        }
    }
}
