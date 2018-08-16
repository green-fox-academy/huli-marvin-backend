using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.EducationControllers
{
    [Route("v1")]
    public class CohortController : Controller
    {
        private readonly ICrudService<CohortDTO, Cohort> cohortService;

        public CohortController(ICrudService<CohortDTO, Cohort> cohortService)
        {
            this.cohortService = cohortService;
        }

        [HttpGet("cohorts")]
        public async Task<IActionResult> ListAll()
        {
            var cohorts = await cohortService.GetAllAsync(Request.Query);
            return Json(cohorts);
        }

        [HttpPost("cohorts")]
        public async Task<IActionResult> CreateCohort([FromBody]Cohort newCohort)
        {
            await cohortService.AddAsync(newCohort);
            return Ok();
        }

        [HttpGet("cohort/{cohortId}")]
        public async Task<IActionResult> FindCohort(long cohortId)
        {
            var cohort = await cohortService.GetByIdAsync(cohortId);
            return Json(cohort);
        }

        [HttpPost("cohort/{cohortId}")]
        public async Task<IActionResult> UpdateCohort(long cohortId, [FromBody]Cohort cohort)
        {
            await cohortService.UpdateAsync(cohortId, cohort);
            return Ok();
        }

        [HttpDelete("cohort/{cohortId}")]
        public async Task<IActionResult> DeleteCohort(long cohortId)
        {
            await cohortService.RemoveByIdAsync(cohortId);
            return Ok();
        }
    }
}
