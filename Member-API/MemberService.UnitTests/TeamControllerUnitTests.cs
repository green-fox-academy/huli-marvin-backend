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
    public class TeamControllerUnitTests
    {
        private Team testTeam = (Team)ModelFactory.Creator<Team>();
        private TeamDTO testTeamDTO = (TeamDTO)ModelFactory.Creator<TeamDTO>();
        private TeamController teamController;

        public TeamControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<TeamDTO, Team>>();

            mockService.Setup(srv => srv.AddAsync(testTeam))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(testTeamDTO));

            mockService.Setup(srv => srv.UpdateAsync(testTeam.Id, testTeam))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testTeam.Id))
               .Returns(Task.CompletedTask);

            teamController = new TeamController(mockService.Object);
        }

        [Fact]
        public async Task CreateTeams_Should_ReturnOk()
        {
            var result = await teamController.CreateNewTeam(testTeam);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindTeam_Should_ReturnTeamDTO_InJson()
        {
            var result = await teamController.FindTeam(testTeamDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testTeamDTO);
        }

        [Fact]
        public async Task UpdateTeam_Should_ReturnOk()
        {
            var result = await teamController.UpdateTeam(testTeam.Id, testTeam);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteTeam_Should_ReturnOk()
        {
            var result = await teamController.DeleteTeam(testTeam.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
