using MemberService.Models.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class JobHistory
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }

        public Profile Profile { get; set; }
    }
}