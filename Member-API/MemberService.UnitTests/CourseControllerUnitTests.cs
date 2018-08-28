using FluentAssertions;
using MemberService.Controllers.EducationControllers;
using MemberService.Factories;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.UnitTests
{
    public class CourseControllerUnitTests
    {
        private readonly Course testCourse = (Course)ModelFactory.Creator<Course>();
        private readonly CourseDTO testCourseDTO = (CourseDTO)ModelFactory.Creator<CourseDTO>();
        private readonly CourseController courseController;

        public CourseControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<CourseDTO, Course>>();

            mockService.Setup(srv => srv.AddAsync(testCourse))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(testCourseDTO));

            mockService.Setup(srv => srv.UpdateAsync(testCourse.Id, testCourse))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testCourse.Id))
               .Returns(Task.CompletedTask);

            courseController = new CourseController(mockService.Object);
        }

        [Fact]
        public async Task CreateCourses_Should_ReturnOk()
        {
            var result = await courseController.CreateCourse(testCourse);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindCourse_Should_ReturnCourseDTO_InJson()
        {
            var result = await courseController.FindCourse(testCourseDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testCourseDTO);
        }

        [Fact]
        public async Task UpdateCourse_Should_ReturnOk()
        {
            var result = await courseController.UpdateCourse(testCourse, testCourse.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCourse_Should_ReturnOk()
        {
            var result = await courseController.DeleteCourse(testCourse.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
