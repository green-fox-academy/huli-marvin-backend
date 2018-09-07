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

        public async Task<EventTemplate> GetItemById(int id)
        {
            return await eventContext.EventTemplates.FindAsync(id);
        }

        //public async List<EventTemplate> GetAll()
        //{
        //    return eventContext.EventTemplates.tol
        //}
    }
}
