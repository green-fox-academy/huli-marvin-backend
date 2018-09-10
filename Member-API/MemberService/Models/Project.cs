using MemberService.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class Project
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Department Department { get; set; }
        public IList<Team> Teams { get; set; } = new List<Team>();
    }
}
