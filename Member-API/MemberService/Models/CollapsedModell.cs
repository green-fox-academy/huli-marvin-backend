namespace MemberService.Models
{
    public struct CollapsedModell
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public CollapsedModell(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}