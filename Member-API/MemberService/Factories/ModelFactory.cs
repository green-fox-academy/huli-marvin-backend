using MemberService.DataTypes.Enums;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.JoinModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MemberService.Factories
{
    public class ModelFactory
    {
        private static long id = 0;
        public static object Creator<T>()
        {
            var methods = typeof(ModelFactory).GetRuntimeMethods();
            return methods.Single(m => m.ReturnType == typeof(T)).Invoke(new ModelFactory(), null);
        }

        private static Class GetClass()
        {
            id++;
            return new Class()
            {
                Id = id,
                Name = $"Class{id}",
                Color = $"#{id}11111",
                Status = (Status)1,
                Course = new Course { Id = id },
                Cohort = new Cohort { Id = id },
                ClassLead = new Profile { Id = id + 10 },
                ClassAdmin = new Profile { Id = id + 20 },
                Mentors = new List<ClassProfile>
                {
                    new ClassProfile { ProfileId = id },
                    new ClassProfile { ProfileId = id + 2 }
                },
                Apprentices = new List<Profile>
                {
                    new Profile { Id = id + 10 },
                    new Profile { Id = id + 20 }
                }
            };
        }

        private static ClassDTO GetClassDTO()
        {
            return new ClassDTO()
            {
                Id = id,
                Name = $"Class{id}",
                Color = $"#{id}11111",
                Status = (Status)1,
                CalendarId = id,
                SlackChannelId = id,
                Cohort = new CollapsedModell(id + 20, $"Cohort{id + 20}"),
                ClassLead = new CollapsedModell(id + 30, $"ClassLead{id + 30}"),
                ClassAdmin = new CollapsedModell(id + 40, $"ClassAdmin{id + 40}"),
                Mentors = new List<CollapsedModell>
                {
                    new CollapsedModell(id, $"Mentor{id}"),
                    new CollapsedModell(id + 2, $"Mentor{id + 2}")
                },
                Apprentices = new List<CollapsedModell>
                {
                    new CollapsedModell(id + 10, $"Apprentice{id + 10}"),
                    new CollapsedModell(id + 20, $"Apprentice{id + 20}")
                }
            };
        }

        private static Course GetCourse()
        {
            id++;
            return new Course()
            {
                Id = id,
                Name = $"Course{id}",
                Status = Status.Active,

                Department = new Department { Id = 21321486, Name = $"Department{id}" },
                Classes = new List<Class>
                {
                    new Class { Id = 21321486, Name = "Class21321486" },
                    new Class { Id = 415812414, Name = "Class415812414" }
                }
            };
        }

        private static CourseDTO GetCourseDTO()
        {
            return new CourseDTO()
            {
                Id = id,
                Name = $"Course{id}",
                Status = Status.Active,

                Department = new CollapsedModell(21321486, $"Department{id}"),
                Classes = new List<CollapsedModell>
                {
                    new CollapsedModell (21321486, "Class21321486"),
                    new CollapsedModell (415812414, "Class415812414")
                }
            };
        }

        private static JobHistory GetJobHistory()
        {
            id++;
            return new JobHistory
            {
                Id = id,
                Name = $"JobHistory{id}",

                Profile = new Profile { Id = id, Name = $"Profile{id}" }
            };
        }

        private static JobHistoryDTO GetJobHistoryDTO()
        {
            return new JobHistoryDTO
            {
                Id = id,
                Name = $"JobHistory{id}",

                Profile = new CollapsedModell(id, $"Profile{id}")
            };
        }

        private static Profile GetProfile()
        {
            id++;
            return new Profile()
            {
                Id = id,
                Name = "Joseph",
                Level = Level.Apprentice,
                Phase = Phase.Project,
                Project = new Project {Name = "Marvin"},

                ClassLeads = new List<Class>
                {
                    new Class { Id = 1, Name = "Class1" },
                    new Class { Id = 2, Name = "Class2" }
                },
                ClassAdmins = new List<Class>
                {
                    new Class { Id = 3,  Name = "Class3"},
                    new Class { Id = 2, Name = "Class2" }
                },
                ClassMentors = new List<ClassProfile>
                {
                    new ClassProfile { ClassId = 1 },
                    new ClassProfile { ClassId = 3 }
                },
                ClassApprentice = null,
                TeamProductOwners = new List<Team>
                {
                    new Team { Id = 1, Name = "Team1" },
                    new Team { Id = 3, Name = "Team3" }
                },
                TeamScrumMasters = new List<Team>
                {
                    new Team { Id = 1, Name = "Team1" },
                    new Team { Id = 2, Name = "Team2" }
                },
                TeamApprentice = new Team { Id = 1, Name = "Chrysocolla" },
                FormerJobs = new List<JobHistory>
                {
                    new JobHistory {Id = 12, Name = "JobHistory12" }
                }
            };
        }

        private static ProfileDTO GetProfileDTO()
        {
            return new ProfileDTO
            {
                Id = id,
                Name = "Joseph",
                Level = Level.Apprentice,
                Phase = Phase.Project,
                Project = new Project { Name = "Marvin" },

                ClassLeads = new List<CollapsedModell>
                {
                    new CollapsedModell( 1, "Class1" ),
                    new CollapsedModell( 2, "Class2")
                },
                ClassAdmins = new List<CollapsedModell>
                {
                    new CollapsedModell(3, "Class3"),
                    new CollapsedModell(2, "Class2")
                },
                ClassMentors = new List<CollapsedModell>
                {
                    new CollapsedModell(1, "Team1"),
                    new CollapsedModell(3, "Team3")
                },
                ClassApprentice = new CollapsedModell(),
                TeamProductOwners = new List<CollapsedModell>
                {
                    new CollapsedModell(1, "Team1"),
                    new CollapsedModell(3, "Team3")
                },
                TeamScrumMasters = new List<CollapsedModell>
                {
                    new CollapsedModell(1, "Team1"),
                    new CollapsedModell(2, "Team2")
                },
                TeamApprentice = new CollapsedModell(1, "Chrysocolla"),
                FormerJobs = new List<CollapsedModell>
                {
                    new CollapsedModell(12, "JobHistory12")
                }
            };
        }

        private static Project GetProject()
        {
            id++;
            return new Project
            {
                Id = id,
                Name = $"Project{id}",
                Description = $"Project{id}",

                Department = new Department { Id = id, Name = $"Department{id}" },
                Teams = new List<Team>()
                {
                    new Team { Id = id, Name = $"Team{id}"},
                    new Team { Id = id + 2, Name =$"Team{id +2}"}
                }
            };
        }

        private static ProjectDTO GetProjectDTO()
        {
            return new ProjectDTO()
            {
                Id = id,
                Name = $"Project{id}",
                Description = $"Project{id}",

                Department = new CollapsedModell(id, $"Department{id}"),
                Teams = new List<CollapsedModell>()
                {
                    new CollapsedModell(id, $"Team{id}"),
                    new CollapsedModell(id + 2, $"Team{id + 2}")
                }
            };
        }

        private static Team GetTeam()
        {
            id++;
            return new Team
            {
                Id = id,
                Name = $"Team{id}",

                Project = new Project { Id = id + 2, Name = $"Project{id + 2}" },
                ProductOwner = new Profile { Id = id, Name = $"Profile{id}" },
                ScrumMaster = new Profile { Id = id + 3, Name = $"Profile{id + 3}" },
                Apprentices = new List<Profile>()
                {
                    new Profile { Id = id + 11, Name = $"Profile{id +11}"},
                    new Profile {Id = id + 21, Name = $"Profile{id +21}"}
                }
            };
        }

        private static TeamDTO GetTeamDTO()
        {
            return new TeamDTO
            {
                Id = id,
                Name = $"Team{id}",

                Project = new CollapsedModell(id + 2, $"Project{id + 2}"),
                ProductOwner = new CollapsedModell(id, $"Profile{id}"),
                ScrumMaster = new CollapsedModell(id, $"Profile{id + 3}"),
                Apprentices = new List<CollapsedModell>()
                {
                    new CollapsedModell(id + 11, $"Profile{id +11}"),
                    new CollapsedModell(id + 21, $"Profile{id +21}")
                }
            };
        }

        private static Cohort GetCohort()
        {
            id++;
            return new Cohort()
            {
                Id = id,
                Name = $"Cohort{id}",
                Status = Status.Active,

                Department = new Department { Id = id, Name = $"Department{id}" },
                Classes = new List<Class>
                {
                    new Class { Id = id, Name = $"Class{id}" },
                    new Class { Id = id + 2, Name = $"Class{id + 2}" }
                }
            };
        }

        private static CohortDTO GetCohortDTO()
        {
            return new CohortDTO()
            {
                Id = id,
                Name = $"Cohort{id}",
                Status = Status.Active,
                Department = new CollapsedModell(id, $"Department{id}"),
                Classes = new List<CollapsedModell>
                {
                    new CollapsedModell(id, $"Class{id}"),
                    new CollapsedModell(id + 2, $"Class{id +2}")
                }
            };
        }

        private static Department GetDepartment()
        {
            id++;
            return new Department()
            {
                Id = id,
                Name = $"Department{id}",
                Address = "Budapest",

                Cohorts = new List<Cohort>
                {
                    new Cohort { Id = id, Name = $"Cohort{id}" },
                    new Cohort { Id = id + 1, Name = $"Cohort{id + 1}" }
                },
                Courses = new List<Course>
                {
                    new Course { Id = id + 2, Name = $"Course{id + 2}" },
                    new Course { Id = id + 3, Name = $"Course{id + 3}" }
                },
                Projects = new List<Project>
                {
                    new Project { Id = id + 4, Name = $"Project{id + 4}" },
                    new Project { Id = id + 5, Name = $"Project{id + 5}" }
                },
            };
        }

        private static DepartmentDTO GetDepartmentDTO()
        {
            return new DepartmentDTO()
            {
                Id = id,
                Name = $"Department{id}",
                Address = "Budapest",

                Cohorts = new List<CollapsedModell>
                {
                    new CollapsedModell(id, $"Cohort{id}"),
                    new CollapsedModell(id + 1, $"Cohort{id + 1}")
                },
                Courses = new List<CollapsedModell>
                {
                    new CollapsedModell(id + 2, $"Course{id + 2}"),
                    new CollapsedModell(id + 3, $"Cohort{id + 3}")
                },
                Projects = new List<CollapsedModell>
                {
                    new CollapsedModell(id + 4, $"Cohort{id + 4}"),
                    new CollapsedModell(id + 5, $"Cohort{id + 5}")
                }
            };
        }
    }
}
