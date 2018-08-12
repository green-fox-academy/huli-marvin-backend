using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventTemplateRepository : IGenericRepository<EventTemplate>
    {
        private EventContext eventContext;

        public EventTemplateRepository(EventContext eventContext)
        {
            this.eventContext = eventContext;
        }

        public void Create(EventTemplate eventTemplates)
        {
            eventContext.Add(eventTemplates);
            eventContext.SaveChanges();
        }

        public List<EventTemplate> Read()
        {
            return eventContext.EventTemplates.ToList();
        }

        public void Update(EventTemplate eventTemplates)
        {
            eventContext.Update(eventTemplates);
            eventContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = GetItemById(id);
            eventContext.Remove(removable);
            eventContext.SaveChanges();
        }

        public EventTemplate GetItemById(int id)
        {
            return eventContext.EventTemplates.ToList().FirstOrDefault(xItem => xItem.EventTemplateId == id);
        }

        public List<EventTemplate> GetAll()
        {
            return eventContext.EventTemplates.ToList();
        }
    }
}
