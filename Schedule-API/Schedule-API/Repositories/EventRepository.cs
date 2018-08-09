using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventRepository
    {
        private EventContext eventContextObj;

        public EventRepository(EventContext eventContextObj)
        {
            this.eventContextObj = eventContextObj;
        }

        public void Create(Events eventClassObj)
        {
            eventContextObj.Add(eventClassObj);
            eventContextObj.SaveChanges();
        }

        public List<Events> Read()
        {
            return eventContextObj.Events.ToList();
        }

        public void Update(Events classObj)
        {
            eventContextObj.Update(classObj);
            eventContextObj.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = eventContextObj.Events.ToList().FirstOrDefault(x => x.Id == id);
            eventContextObj.Remove(removable);
            eventContextObj.SaveChanges();
        }

        public Events GetItemById(long id)
        {
            return eventContextObj.Events.ToList().FirstOrDefault(x => x.Id == id);
        }
    }
}
