using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using MemberService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Configurations
{
    public static class ServiceConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICrudService<ProfileDTO, Profile>, ProfileService>();            
            services.AddScoped<ICrudService<ClassDTO, Class>, ClassService>();            
            services.AddScoped<ICrudService<DepartmentDTO, Department>, DepartmentService>();            
            services.AddScoped<ICrudService<CohortDTO, Cohort>, CohortService>();            
            services.AddScoped<ICrudService<CourseDTO, Course>, CourseService>();            
            services.AddScoped<ICrudService<TeamDTO, Team>, TeamService>();            
            services.AddScoped<ICrudService<ProjectDTO, Project>, ProjectService>();            
            services.AddScoped<ICrudService<JobHistoryDTO, JobHistory>, JobHistoryService>();
            services.AddScoped<IReadService<AttendanceInfoDTO>, AttendanceService>();
            services.AddScoped<AuthService>();
            services.AddScoped<IStorageService, StorageService>();
        }
    }
}
