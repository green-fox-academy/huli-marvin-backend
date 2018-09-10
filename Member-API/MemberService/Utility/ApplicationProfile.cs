using MemberService.Models;
using MemberService.Models.DataTransferObjects;

namespace MemberService.Utility
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Class, ClassDTO>();
            CreateMap<Cohort, CohortDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<JobHistory, JobHistoryDTO>();
            CreateMap<Profile, ProfileDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<Team, TeamDTO>();

            CreateMap<ClassDTO, Class>();
            CreateMap<CohortDTO, Cohort>();
            CreateMap<CourseDTO, Course>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<JobHistoryDTO, JobHistory>();
            CreateMap<ProfileDTO, Profile>();
            CreateMap<ProjectDTO, Project>();
            CreateMap<TeamDTO, Team>();
        }
    }
}
