using MemberService.DataTypes.Enums;
using MemberService.Models.JoinModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class Profile
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GitHubUser { get; set; }
        public string SlackUser { get; set; }
        public string LinkedIn { get; set; }
        public Education Education { get; set; }
        public bool IsSigned { get; set; }
        public Payment Payment { get; set; }
        public Phase Phase { get; set; }

        public AttendanceInfo AttendanceInfo { get; set; }
        public Project Project { get; set; }
        public string Picture { get; set; }
      
        public Cohort CohortApprentice { get; set; }
        public IList<Class> ClassLeads { get; set; } = new List<Class>();
        public IList<Class> ClassAdmins { get; set; } = new List<Class>();
        public IList<ClassProfile> ClassMentors { get; set; } = new List<ClassProfile>();
        public Class ClassApprentice { get; set; }
        public IList<Team> TeamProductOwners { get; set; } = new List<Team>();
        public IList<Team> TeamScrumMasters { get; set; } = new List<Team>();
        public Team TeamApprentice { get; set; }
        public IList<JobHistory> FormerJobs { get; set; } = new List<JobHistory>();

        public long? GetTeamApprenticeId(Team apprentice)
        {
            return apprentice != null ? apprentice.Id : (long?)null;
        }

        public long? GetClassApprenticeId(Class apprentice)
        {
            return apprentice != null ? apprentice.Id : (long?)null;
        }
    }
}
