using MemberService.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class Team
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }

        public Project Project { get; set; }
        public Profile ProductOwner { get; set; }
        public Profile ScrumMaster { get; set; }
        public IList<Profile> Apprentices { get; set; } = new List<Profile>();
    }
}
