namespace MemberService.Models.DataTransferObjects
{
    public class AttendanceSummaryDTO
    {
        public int UserId { get; set; }
        public int Late { get; set; }
        public int DayOff { get; set; }
        public int SickVerified { get; set; }
        public int SickUnverified { get; set; }
    }
}
