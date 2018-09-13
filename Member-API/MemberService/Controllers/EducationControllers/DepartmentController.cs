using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.EducationControllers
{
    [Route("v1")]
    public class DepartmentController : Controller
    {
        private readonly ICrudService<DepartmentDTO, Department> departmentService;

        public DepartmentController(ICrudService<DepartmentDTO, Department> departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet("departments")]
        public async Task<IActionResult> ListDepartments()
        {
            var departments = await departmentService.GetAllAsync(Request.Query);
            return Json(departments);
        }

        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment([FromBody]Department newDepartment)
        {
            await departmentService.AddAsync(newDepartment);
            return Ok();
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> FindDepartment(long departmentId)
        {
            var departments = await departmentService.GetByIdAsync(departmentId);
            return Json(departments);
        }

        [HttpPut("department/{departmentId}")]
        public async Task<IActionResult> UpdateDepartment(long departmentId, [FromBody]Department updateDepartment)
        {
            await departmentService.UpdateAsync(departmentId, updateDepartment);
            return Ok();
        }

        [HttpDelete("department/{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(long departmentId)
        {
            await departmentService.RemoveByIdAsync(departmentId);
            return Ok();
        }
    }
}