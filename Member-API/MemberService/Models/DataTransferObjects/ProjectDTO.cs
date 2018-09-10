using System.Collections.Generic;

namespace MemberService.Models.DataTransferObjects
{
    public class ProjectDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CollapsedModell Department { get; set; }
        public IList<CollapsedModell> Teams { get; set; }
    }
}
