using System.Threading.Tasks;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.Controllers.ProfileControllers
{
    [Route("v1")]
    public class AttendanceController : Controller
    {
        private readonly IReadService<AttendanceInfoDTO> attendanceService;

        public AttendanceController(IReadService<AttendanceInfoDTO> attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        [HttpGet("attendances")]
        public async Task<IActionResult> ListAttendances()
        {
            var attendances = await attendanceService.GetAllAsync(Request.Query);
            return Json(attendances);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendanceSummaryAsync([FromRoute] int id, [FromBody] AttendanceSummaryDTO attendanceSummary)
        {
            var attendanceInfo = new AttendanceInfo(attendanceSummary);
            //await attendanceService.UpdateAsync(id, attendanceInfo);
            return Json("ok");
        }
    }
}
