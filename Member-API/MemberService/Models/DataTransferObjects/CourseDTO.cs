using MemberService.DataTypes.Enums;
using System.Collections.Generic;

namespace MemberService.Models.DataTransferObjects
{
    public class CourseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        public CollapsedModell Department { get; set; }
        public IList<CollapsedModell> Classes { get; set; }
    }
}
