using System.Collections.Generic;

namespace MemberService.Models.DataTransferObjects
{
    public class DepartmentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string ZipNumber { get; set; }
        public string Address { get; set; }

        public IList<CollapsedModell> Cohorts { get; set; }
        public IList<CollapsedModell> Courses { get; set; }
        public IList<CollapsedModell> Projects { get; set; }
    }
}
