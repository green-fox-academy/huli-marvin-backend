using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAPI.Repositories
{
    public interface IGenericRepository<Object>
    {
        Task CreateAsync(Object key);
        Task UpdateAsync(Object key);
        Task DeleteAsync(int id);
        Task<Object> GetItemByIdAsync(int id);
        IEnumerable<Object> GetAll(int pageSize, int pageIndex);
    }
}
