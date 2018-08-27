using MemberService.Configurations;
using MemberService.Entities;
using MemberService.Extensions;
using MemberService.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace MemberService
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }
        public IHostingEnvironment CurrentEnvironment { get; set; }

        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddServices();
            services.AddRepositories();
            services.AddMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Marvin API", Version = "v1" });
            });

            services.AddDbContext<MemberContext>(options => options.BuildConnection(Configuration));
            services.AddAuth(Configuration);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddServices();
            services.AddRepositories();
            services.AddMapper();

            services.AddDbContext<MemberContext>(options =>
            options.UseInMemoryDatabase("testdatabase"));
        }

        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}