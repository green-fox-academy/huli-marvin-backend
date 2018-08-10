using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventRepository : IGenericRepository<Events>
    {
        private EventContext eventContextObj;

        public EventRepository(EventContext eventContextObj)
        {
            this.eventContextObj = eventContextObj;
        }

        public void Create(Events events)
        {
            eventContextObj.Add(events);
            eventContextObj.SaveChanges();
        }

        public List<Events> Read()
        {
            return eventContextObj.Events.ToList();
        }

        public void Update(Events events)
        {
            eventContextObj.Update(events);
            eventContextObj.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = eventContextObj.Events.ToList().FirstOrDefault(x => x.Id == id);
            eventContextObj.Remove(removable);
            eventContextObj.SaveChanges();
        }

        public Events GetItemById(int id)
        {
            return eventContextObj.Events.ToList().FirstOrDefault(x => x.Id == id);
        }
    }
}
