using MemberService.Models;
using MemberService.Models.Interfaces;
using MemberService.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Configurations
{
    public static class RepositoryConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository<Profile>, ProfileRepository>();            
            services.AddScoped<ICrudRepository<Class>, ClassRepository>();            
            services.AddScoped<ICrudRepository<Department>, DepartmentRepository>();            
            services.AddScoped<ICrudRepository<Cohort>, CohortRepository>();            
            services.AddScoped<ICrudRepository<Course>, CourseRepository>();            
            services.AddScoped<ICrudRepository<Team>, TeamRepository>();            
            services.AddScoped<ICrudRepository<Project>, ProjectRepository>();            
            services.AddScoped<ICrudRepository<JobHistory>, JobHistoryRepository>();
        }
    }
}
