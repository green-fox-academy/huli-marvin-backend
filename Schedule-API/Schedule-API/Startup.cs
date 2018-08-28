using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleAPI.Extensions;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;
using Swashbuckle.AspNetCore.Swagger;

namespace Schedule_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionStringToDoDB = Configuration.GetConnectionString("connString");
            services.AddMvc();

            services.AddDbContext<EventContext>(options =>
                options.UseSqlServer(connectionStringToDoDB));

            services.AddTransient<EventContext>();
            services.AddTransient<EventRepository>();
            services.AddTransient<EventTemplateRepository>();
            services.AddTransient<EventService>();
            services.AddTransient<EventViewModel>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MarvinEvent", Version = "v1" });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateMyModelAttribute));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarvinEvent V1");
            });

            app.UseStaticFiles();
        }
    }
}
