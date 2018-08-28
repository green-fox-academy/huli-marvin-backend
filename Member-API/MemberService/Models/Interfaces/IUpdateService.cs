using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberService.Models.Interfaces
{
    public interface IUpdateService<U> where U : class
    {
        Task UpdateAsync(long id, U entity);
    }
}
