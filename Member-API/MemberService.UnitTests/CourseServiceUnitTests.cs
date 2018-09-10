using AutoMapper;
using FluentAssertions;
using MemberService.Factories;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Exceptions;
using MemberService.Models.Interfaces;
using MemberService.Services;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.UnitTests
{
    public class CourseServiceUnitTests
    {
        private readonly Course testCourse = (Course)ModelFactory.Creator<Course>();
        private readonly CourseDTO testCourseDTO = (CourseDTO)ModelFactory.Creator<CourseDTO>();
        private readonly CourseService courseService;
        private readonly IEnumerable<Course> courses;
        private readonly Mock<ICrudRepository<Course>> mockCourseRepo = new Mock<ICrudRepository<Course>>();
        
        public CourseServiceUnitTests()
        {
            courses = new List<Course>() { testCourse };

            mockCourseRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(courses));

            mockCourseRepo.Setup(r => r.SelectByIdAsync(testCourse.Id))
                .Returns(Task.FromResult(testCourse));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<CourseDTO>(It.IsAny<Course>())).Returns(testCourseDTO);

            courseService = new CourseService(mockCourseRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCourseDTO()
        {
            var expected = new List<CourseDTO>() { testCourseDTO };
            var result = await courseService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => courseService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameCourse()
        {
            await courseService.AddAsync(testCourse);
            mockCourseRepo.Verify(r => r.InsertAsync(testCourse), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCourseDTO()
        {
            var expected = testCourseDTO;
            var result = await courseService.GetByIdAsync(testCourse.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameCourse()
        {
            await courseService.UpdateAsync(testCourse.Id, testCourse);
            mockCourseRepo.Verify(r => r.UpdateAsync(testCourse), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => courseService.UpdateAsync(testId, testCourse));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await courseService.RemoveByIdAsync(testCourse.Id);
            mockCourseRepo.Verify(r => r.DeleteByIdAsync(testCourse.Id), Times.Once());
        }
    }
}
