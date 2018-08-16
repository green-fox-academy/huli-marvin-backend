using MemberService.DataTypes.Enums;
using System.Collections.Generic;

namespace MemberService.Models.DataTransferObjects
{
    public class ClassDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Status Status { get; set; }
        public long CalendarId { get; set; }
        public long SlackChannelId { get; set; }

        public CollapsedModell Course { get; set; }
        public CollapsedModell Cohort { get; set; }
        public CollapsedModell ClassLead { get; set; }
        public CollapsedModell ClassAdmin { get; set; }
        public IList<CollapsedModell> Mentors { get; set; } 
        public IList<CollapsedModell> Apprentices { get; set; }
    }
}
