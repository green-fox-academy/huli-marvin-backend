using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.EducationControllers
{
    [Route("v1")]
    public class CourseController : Controller
    {
        private readonly ICrudService<CourseDTO, Course> courseService;

        public CourseController(ICrudService<CourseDTO, Course> courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> ListCourses()
        {
            var courses = await courseService.GetAllAsync(Request.Query);
            return Json(courses);
        }

        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse([FromBody]Course newCourse)
        {
            await courseService.AddAsync(newCourse);
            return Ok();
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> FindCourse(long id)
        {
            var course = await courseService.GetByIdAsync(id);
            return Json(course);
        }

        [HttpPut("course/{courseId}")]
        public async Task<IActionResult> UpdateCourse([FromBody] Course updateCourse, long id)
        {
            await courseService.UpdateAsync(id, updateCourse);
            return Ok();
        }

        [HttpDelete("course/{courseId}")]
        public async Task<IActionResult> DeleteCourse(long courseId)
        {
            await courseService.RemoveByIdAsync(courseId);
            return Ok();
        }
    }
}
