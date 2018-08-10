using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventTemplateRepository : IGenericRepository<EventTemplates>
    {
        private EventContext ContextObj;

        public EventTemplateRepository(EventContext ContextObj)
        {
            this.ContextObj = ContextObj;
        }

        public void Create(EventTemplates eventTemplates)
        {
            ContextObj.Add(eventTemplates);
            ContextObj.SaveChanges();
        }

        public List<EventTemplates> Read()
        {
            return ContextObj.EventTemplates.ToList();
        }

        public void Update(EventTemplates eventTemplates)
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

        public EventTemplates GetItemById(int id)
        {
            return ContextObj.EventTemplates.ToList().FirstOrDefault(xItem => xItem.Id == id);
        }
    }
}
