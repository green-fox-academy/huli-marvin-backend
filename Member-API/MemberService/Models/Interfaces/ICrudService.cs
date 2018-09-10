using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberService.Models.Interfaces
{
    public interface ICrudService<T, U> : IReadService<T>, IUpdateService<U> where T : class
                                                                             where U : class
    {
        Task AddAsync(U entity);
        Task RemoveByIdAsync(long id);
    }
}