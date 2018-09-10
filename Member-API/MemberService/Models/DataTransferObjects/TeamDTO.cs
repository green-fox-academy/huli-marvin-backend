using System.Collections.Generic;

namespace MemberService.Models.DataTransferObjects
{
    public class TeamDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public CollapsedModell Project { get; set; }
        public CollapsedModell ProductOwner { get; set; }
        public CollapsedModell ScrumMaster { get; set; }
        public IList<CollapsedModell> Apprentices { get; set; }
    }
}