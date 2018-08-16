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
    public class ProjectServiceTests
    {
        private readonly Project testProject = (Project)ModelFactory.Creator<Project>();
        private readonly ProjectDTO testProjectDTO = (ProjectDTO)ModelFactory.Creator<ProjectDTO>();
        private readonly ProjectService projectService;
        private readonly IEnumerable<Project> projects;
        private readonly Mock<ICrudRepository<Project>> mockProjectRepo = new Mock<ICrudRepository<Project>>();

        public ProjectServiceTests()
        {
            projects = new List<Project>() { testProject };

            mockProjectRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(projects));

            mockProjectRepo.Setup(r => r.SelectByIdAsync(testProject.Id))
                .Returns(Task.FromResult(testProject));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<ProjectDTO>(It.IsAny<Project>())).Returns(testProjectDTO);

            projectService = new ProjectService(mockProjectRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCourseDTO()
        {
            var expected = new List<ProjectDTO>() { testProjectDTO };
            var result = await projectService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => projectService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameCourse()
        {
            await projectService.AddAsync(testProject);
            mockProjectRepo.Verify(r => r.InsertAsync(testProject), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCourseDTO()
        {
            var expected = testProjectDTO;
            var result = await projectService.GetByIdAsync(testProject.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameCourse()
        {
            await projectService.UpdateAsync(testProject.Id, testProject);
            mockProjectRepo.Verify(r => r.UpdateAsync(testProject), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => projectService.UpdateAsync(testId, testProject));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await projectService.RemoveByIdAsync(testProject.Id);
            mockProjectRepo.Verify(r => r.DeleteByIdAsync(testProject.Id), Times.Once());
        }
    }
}
