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
    public class TeamServiceTests
    {
        private readonly Team testTeam = (Team)ModelFactory.Creator<Team>();
        private readonly TeamDTO testTeamDTO = (TeamDTO)ModelFactory.Creator<TeamDTO>();
        private readonly TeamService teamService;
        private readonly IEnumerable<Team> teams;
        private readonly Mock<ICrudRepository<Team>> mockTeamRepo = new Mock<ICrudRepository<Team>>();

        public TeamServiceTests()
        {
            teams = new List<Team>() { testTeam };

            mockTeamRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(teams));

            mockTeamRepo.Setup(r => r.SelectByIdAsync(testTeam.Id))
                .Returns(Task.FromResult(testTeam));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<TeamDTO>(It.IsAny<Team>())).Returns(testTeamDTO);

            teamService = new TeamService(mockTeamRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCourseDTO()
        {
            var expected = new List<TeamDTO>() { testTeamDTO };
            var result = await teamService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => teamService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameCourse()
        {
            await teamService.AddAsync(testTeam);
            mockTeamRepo.Verify(r => r.InsertAsync(testTeam), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCourseDTO()
        {
            var expected = testTeamDTO;
            var result = await teamService.GetByIdAsync(testTeam.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameCourse()
        {
            await teamService.UpdateAsync(testTeam.Id, testTeam);
            mockTeamRepo.Verify(r => r.UpdateAsync(testTeam), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => teamService.UpdateAsync(testId, testTeam));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await teamService.RemoveByIdAsync(testTeam.Id);
            mockTeamRepo.Verify(r => r.DeleteByIdAsync(testTeam.Id), Times.Once());
        }
    }
}
