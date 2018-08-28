using MemberService.DataTypes.Enums;
using System.Collections.Generic;

namespace MemberService.Models.DataTransferObjects
{
    public class ProfileDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string GitHubUser { get; set; }
        public string SlackUser { get; set; }
        public string LinkedIn { get; set; }
        public Education Education { get; set; }
        public bool IsSigned { get; set; }
        public Payment Payment { get; set; }
        public string Picture { get; set; }
        public Phase Phase { get; set; }
        public AttendanceInfo AttendanceInfo { get; set; }
        public Project Project { get; set; }

        public CollapsedModell CohortApprentice { get; set; }
        public IList<CollapsedModell> ClassLeads { get; set; }
        public IList<CollapsedModell> ClassAdmins { get; set; }
        public IList<CollapsedModell> ClassMentors { get; set; }
        public CollapsedModell ClassApprentice { get; set; }
        public IList<CollapsedModell> TeamProductOwners { get; set; }
        public IList<CollapsedModell> TeamScrumMasters { get; set; }
        public CollapsedModell TeamApprentice { get; set; }
        public IList<CollapsedModell> FormerJobs { get; set; }
    }
}
