using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventTemplateRepository : IGenericRepository<EventTemplate>
    {
        private EventContext ContextObj;

        public EventTemplateRepository(EventContext ContextObj)
        {
            this.ContextObj = ContextObj;
        }

        public void Create(EventTemplate eventTemplates)
        {
            ContextObj.Add(eventTemplates);
            ContextObj.SaveChanges();
        }

        public List<EventTemplate> Read()
        {
            return ContextObj.EventTemplates.ToList();
        }

        public void Update(EventTemplate eventTemplates)
        {
            ContextObj.Update(eventTemplates);
            ContextObj.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = GetItemById(id);
            ContextObj.Remove(removable);
            ContextObj.SaveChanges();
        }

        public EventTemplate GetItemById(int id)
        {
            return ContextObj.EventTemplates.ToList().FirstOrDefault(xItem => xItem.Id == id);
        }
    }
}
