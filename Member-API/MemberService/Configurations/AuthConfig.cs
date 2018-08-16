using MemberService.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Configurations
{
    public static class AuthConfig
    {
        public static void AddAuth(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddGoogle(options =>
            {
                options.ClientId = Configuration["ClientId"];
                options.ClientSecret = Configuration["ClientSecret"];
            })
            .AddJwtBearer(options =>
            {
                options.GenerateValidationParameters(Configuration);
            });
        }
    }
}
