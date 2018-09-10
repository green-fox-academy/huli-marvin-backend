namespace MemberService.Models.DataTransferObjects
{
    public class AttendanceInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int DayOff { get; set; }
        public int Late { get; set; }
        public int SickVerified { get; set; }
        public int SickUnverified { get; set; }
        public string Class { get; set; }
        public string Cohort { get; set; }
    }
}
