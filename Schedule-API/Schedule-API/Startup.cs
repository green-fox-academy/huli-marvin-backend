using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScheduleAPI.Models;
using ScheduleAPI.Repositories;
using ScheduleAPI.Services;
using ScheduleAPI.ViewModels;

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
            services.AddTransient<EventService>();
            services.AddTransient<EventViewModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
