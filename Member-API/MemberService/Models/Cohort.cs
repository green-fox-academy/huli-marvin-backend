using MemberService.DataTypes.Enums;
using MemberService.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class Cohort
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public Status Status { get; set; }

        public Department Department { get; set; }
        public IList<Class> Classes { get; set; } = new List<Class>();
    }
}
