using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.HuliControllers
{
    [Route("v1")]
    public class ProjectController : Controller
    {
        private readonly ICrudService<ProjectDTO, Project> projectService;

        public ProjectController(ICrudService<ProjectDTO, Project> projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("projects")]
        public async Task<IActionResult> ListProjects()
        {
            var projects = await projectService.GetAllAsync(Request.Query);
            return Json(projects);
        }

        [HttpPost("projects")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            await projectService.AddAsync(project);
            return Ok();
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> FindProject(long id)
        {
            var projectId = await projectService.GetByIdAsync(id);
            return Json(projectId);
        }

        [HttpPut("project/{projectId}")]
        public async Task<IActionResult> UpdateProject([FromBody] Project updateProject, long id)
        {
            await projectService.UpdateAsync(id, updateProject);
            return Ok();
        }

        [HttpDelete("project/{projectId}")]
        public async Task<IActionResult> DeleteProject(long projectId)
        {
            await projectService.RemoveByIdAsync(projectId);
            return Ok();
        }
    }
}
