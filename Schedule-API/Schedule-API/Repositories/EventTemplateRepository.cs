using ScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public class EventTemplateRepository
    {
        private EventContext ContextObj;

        public EventTemplateRepository(EventContext ContextObj)
        {
            this.ContextObj = ContextObj;
        }

        public void Create(EventTemplates classObj)
        {
            ContextObj.Add(classObj);
            ContextObj.SaveChanges();
        }

        public List<EventTemplates> Read()
        {
            return ContextObj.EventTemplates.ToList();
        }

        public void Update(EventTemplates classObj)
        {
            ContextObj.Update(classObj);
            ContextObj.SaveChanges();
        }

        public void Delete(int id)
        {
            var removable = GetItemById(id);
            ContextObj.Remove(removable);
            ContextObj.SaveChanges();
        }

        public EventTemplates GetItemById(long id)
        {
            return ContextObj.EventTemplates.ToList().FirstOrDefault(xItem => xItem.Id == id);
        }
    }
}
