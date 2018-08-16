using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.EducationControllers
{
    [Route("v1")]
    public class ClassController : Controller
    {
        private readonly ICrudService<ClassDTO, Class> classService;

        public ClassController(ICrudService<ClassDTO, Class> classService)
        {
            this.classService = classService;
        }

        [HttpGet("classes")]
        public async Task<IActionResult> ListClasses()
        {
            var classes = await classService.GetAllAsync(Request.Query);
            return Json(classes);
        }

        [HttpPost("classes")]
        public async Task<IActionResult> CreateClass([FromBody] Class newClass)
        {
            await classService.AddAsync(newClass);
            return Ok();
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> FindClass(long classId)
        {
            var result = await classService.GetByIdAsync(classId);
            return Json(result);
        }

        [HttpPost("class/{classId}")]
        public async Task<IActionResult> UpdateClass(long classId, [FromBody] Class updatedClass)
        {
            await classService.UpdateAsync(classId, updatedClass);
            return Ok();
        }

        [HttpDelete("class/{classId}")]
        public async Task<IActionResult> DeleteClass(long classId)
        {
            await classService.RemoveByIdAsync(classId);
            return Ok();
        }
    }
}