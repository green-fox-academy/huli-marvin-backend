using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MemberService.Extensions
{
    public static class QueryValidator<T> where T : class
    {
        public static bool IsAllPropertyValid(IQueryCollection query)
        {
            return query != null && query.All(queryPart => typeof(T).GetProperty(queryPart.Key) != null);
        }
    }
}
