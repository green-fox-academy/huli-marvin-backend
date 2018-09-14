using System.ComponentModel.DataAnnotations;
using MemberService.Models.DataTransferObjects;

namespace MemberService.Models
{
    public class AttendanceInfo
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public int Late { get; set; }
        [Required]
        public int DayOff { get; set; }
        [Required]
        public int SickVerified { get; set; }
        [Required]
        public int SickUnverified { get; set; }

        public AttendanceInfo(long id, int late, int dayOff, int sickVerified, int sickUnverified)
        {
            Id = id;
            Late = late;
            DayOff = dayOff;
            SickVerified = sickVerified;
            SickUnverified = sickUnverified;
        }

        public AttendanceInfo(AttendanceSummaryDTO entity)
        {
            CreateFromAttendanceSummary(entity);
        }

        private void CreateFromAttendanceSummary(AttendanceSummaryDTO entity)
        {
            Late = entity.Late;
            DayOff = entity.DayOff;
            SickVerified = entity.SickVerified;
            SickUnverified = entity.SickUnverified;
        }
    }
}
