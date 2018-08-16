using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.ProfileControllers
{
    [Route("v1")]
    public class JobHistoryController : Controller
    {
        private readonly ICrudService<JobHistoryDTO, JobHistory> jobHistoryService;

        public JobHistoryController(ICrudService<JobHistoryDTO, JobHistory> jobHistoryService)
        {
            this.jobHistoryService = jobHistoryService;
        }

        [HttpGet("jobhistories")]
        public async Task<IActionResult> ListAll()
        {
            var jobHistories = await jobHistoryService.GetAllAsync(Request.Query);
            return Json(jobHistories);
        }

        [HttpPost("jobhistories")]
        public async Task<IActionResult> CreateJobHistory([FromBody]JobHistory newJobHistory)
        {
            await jobHistoryService.AddAsync(newJobHistory);
            return Ok();
        }

        [HttpGet("jobhistory/{jobhistoryId}")]
        public async Task<IActionResult> FindJobHistory(long jobHistoryId)
        {
            var jobHistory = await jobHistoryService.GetByIdAsync(jobHistoryId);
            return Json(jobHistory);
        }

        [HttpPost("jobhistory/{jobhistoryId}")]
        public async Task<IActionResult> UpdateJobHistory(long jobHistoryId, [FromBody]JobHistory jobHistory)
        {
            await jobHistoryService.UpdateAsync(jobHistoryId, jobHistory);
            return Ok();
        }

        [HttpDelete("jobhistory/{jobhistoryId}")]
        public async Task<IActionResult> DeleteJobHistory(long jobHistoryId)
        {
            await jobHistoryService.RemoveByIdAsync(jobHistoryId);
            return Ok();
        }
    }
}
