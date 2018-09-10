using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MemberService.Extensions
{
    public static class ObjectStringContentConverter
    {
        public static StringContent ToJsonContent(this object o)
        {
            var dataAsString = JsonConvert.SerializeObject(o);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
