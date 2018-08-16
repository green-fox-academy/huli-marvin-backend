namespace MemberService.Models.DataTransferObjects
{
    public class JobHistoryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public CollapsedModell Profile { get; set; }
    }
}