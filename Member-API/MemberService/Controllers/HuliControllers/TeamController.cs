using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.HuliControllers
{
    [Route("v1")]
    public class TeamController : Controller
    {
        private readonly ICrudService<TeamDTO, Team> teamService;

        public TeamController(ICrudService<TeamDTO, Team> teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet("teams")]
        public async Task<IActionResult> ListTeams()
        {
            var teams = await teamService.GetAllAsync(Request.Query);
            return Json(teams);
        }

        [HttpPost("teams")]
        public async Task<IActionResult> CreateNewTeam([FromBody]Team newTeam)
        {
            await teamService.AddAsync(newTeam);
            return Ok();
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> FindTeam(long teamId)
        {
            var teams = await teamService.GetByIdAsync(teamId);
            return Json(teams);
        }

        [HttpPut("team/{teamId}")]
        public async Task<IActionResult> UpdateTeam(long teamId, [FromBody] Team updateTeam)
        {
            await teamService.UpdateAsync(teamId,updateTeam);
            return Ok();
        }

        [HttpDelete("team/{teamId}")]
        public async Task<IActionResult> DeleteTeam(long teamId)
        {
            await teamService.RemoveByIdAsync(teamId);
            return Ok();
        }
    }
}
