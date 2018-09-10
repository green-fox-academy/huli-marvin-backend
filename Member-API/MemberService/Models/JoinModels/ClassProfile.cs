namespace MemberService.Models.JoinModels
{
    public class ClassProfile
    {
        public long ClassId { get; set; }
        public Class Class { get; set; }

        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
