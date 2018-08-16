using MemberService.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Configurations
{
    public static class MapperConfig
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile<ApplicationProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
