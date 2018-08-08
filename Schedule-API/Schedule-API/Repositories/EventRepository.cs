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

        //public void Create(tableclass eventClassObj)
        //{
        //    eventContextObj.Add(eventClassObj);
        //    eventContextObj.SaveChanges();
        //}

        //public List<tableclass> Read()
        //{
        //    return eventContextObj.tableName.ToList();
        //}

        //public void Update(tableclass classObj)
        //{
        //    eventContextObj.Update(classObj);
        //    eventContextObj.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    var removable = eventContextObj.tableName.ToList().FirstOrDefault(x => x.Id == id);
        //    eventContextObj.Remove(removable);
        //    eventContextObj.SaveChanges();
        //}

        //public tableclass GetTodoById(long id)
        //{
        //    return eventContextObj.tableName.ToList().FirstOrDefault(x => x.Id == id);
        //}
    }
}
