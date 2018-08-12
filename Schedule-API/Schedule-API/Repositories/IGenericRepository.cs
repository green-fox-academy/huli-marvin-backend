using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public interface IGenericRepository<Object>
    {
        List<Object> Read();
        void Create(Object key);
        void Update(Object key);
        void Delete(int id);
        Object GetItemById(int id);
    }
}
