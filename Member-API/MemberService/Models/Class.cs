using MemberService.DataTypes.Enums;
using MemberService.Models.Exceptions;
using MemberService.Models.JoinModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class Class
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Status Status { get; set;}
        public long CalendarId { get; set; }
        public long SlackChannelId { get; set; }

        public Course Course { get; set; }
        public Cohort Cohort { get; set; }
        public Profile ClassLead { get; set; }
        public Profile ClassAdmin { get; set; }
        public IList<ClassProfile> Mentors { get; set; } = new List<ClassProfile>();
        public IList<Profile> Apprentices { get; set; } = new List<Profile>();
    }
}
