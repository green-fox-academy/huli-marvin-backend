using System.Threading.Tasks;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.Controllers.ProfileControllers
{
    [Route("attendance")]
    public class AttendanceController : Controller
    {
        private readonly ICrudService<AttendanceInfoDTO, AttendanceInfo> attendanceService;

        public AttendanceController(ICrudService<AttendanceInfoDTO, AttendanceInfo> attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAttendanceAsync()
        {
            var result = await attendanceService.GetAllAsync(Request.Query);
            return Json(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendanceSummaryAsync([FromRoute] int id, [FromBody] AttendanceSummaryDTO attendanceSummary)
        {
            var attendanceInfo = new AttendanceInfo(attendanceSummary);
            await attendanceService.UpdateAsync(id, attendanceInfo);
            return Json("ok");
        }
    }
}
