using FluentAssertions;
using MemberService.Controllers.HuliControllers;
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
    public class ProjectControllerUnitTests
    {
        private Project testProject = (Project)ModelFactory.Creator<Project>();
        private ProjectDTO testProjectDTO = (ProjectDTO)ModelFactory.Creator<ProjectDTO>();
        private ProjectController projectController;

        public ProjectControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<ProjectDTO, Project>>();

            mockService.Setup(srv => srv.AddAsync(testProject))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(testProjectDTO));

            mockService.Setup(srv => srv.UpdateAsync(testProject.Id, testProject))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testProject.Id))
               .Returns(Task.CompletedTask);

            projectController = new ProjectController(mockService.Object);
        }

        [Fact]
        public async Task CreateProjects_Should_ReturnOk()
        {
            var result = await projectController.CreateProject(testProject);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindProject_Should_ReturnProjectDTO_InJson()
        {
            var result = await projectController.FindProject(testProjectDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testProjectDTO);
        }

        [Fact]
        public async Task UpdateProject_Should_ReturnOk()
        {
            var result = await projectController.UpdateProject(testProject, testProject.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteProject_Should_ReturnOk()
        {
            var result = await projectController.DeleteProject(testProject.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
