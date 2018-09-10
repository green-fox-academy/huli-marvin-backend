using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MemberService.Models.Interfaces
{
    public interface IReadService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(IQueryCollection q);
        Task<T> GetByIdAsync(long id);
    }
}
