using MemberService.Models.Exceptions;
using System;
using System.Collections.Generic;

namespace MemberService.Models
{
    public class Department
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string ZipNumber { get; set; }
        public string Address { get; set; }

        public IList<Cohort> Cohorts { get; set; } = new List<Cohort>();
        public IList<Course> Courses { get; set; } = new List<Course>();
        public IList<Project> Projects { get; set; } = new List<Project>();
    }
}
