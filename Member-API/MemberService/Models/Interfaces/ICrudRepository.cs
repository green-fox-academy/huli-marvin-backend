using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberService.Models.Interfaces
{
    public interface ICrudRepository<T> where T : class 
    {
        Task<IEnumerable<T>> SelectAllAsync(IQueryCollection q);
        Task InsertAsync(T entity);
        Task<T> SelectByIdAsync(long id);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(long id);
        Task LoadContextAsync();
    }
}