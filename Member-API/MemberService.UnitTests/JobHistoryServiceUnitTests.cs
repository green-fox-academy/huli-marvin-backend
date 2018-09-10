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
    public class JobHistoryServiceUnitTests
    {
        private readonly JobHistory testJobHistory = (JobHistory)ModelFactory.Creator<JobHistory>();
        private readonly JobHistoryDTO testJobHistoryDTO = (JobHistoryDTO)ModelFactory.Creator<JobHistoryDTO>();
        private readonly JobHistoryService jobHistoryService;
        private readonly IEnumerable<JobHistory> jobHistories;
        private readonly Mock<ICrudRepository<JobHistory>> mockJobHistoryRepo = new Mock<ICrudRepository<JobHistory>>();

        public JobHistoryServiceUnitTests()
        {
            jobHistories = new List<JobHistory>() { testJobHistory };

            mockJobHistoryRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(jobHistories));

            mockJobHistoryRepo.Setup(r => r.SelectByIdAsync(testJobHistory.Id))
                .Returns(Task.FromResult(testJobHistory));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<JobHistoryDTO>(It.IsAny<JobHistory>())).Returns(testJobHistoryDTO);

            jobHistoryService = new JobHistoryService(mockJobHistoryRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCourseDTO()
        {
            var expected = new List<JobHistoryDTO>() { testJobHistoryDTO };
            var result = await jobHistoryService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => jobHistoryService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameJobHistory()
        {
            await jobHistoryService.AddAsync(testJobHistory);
            mockJobHistoryRepo.Verify(r => r.InsertAsync(testJobHistory), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnJobHistoryDTO()
        {
            var expected = testJobHistoryDTO;
            var result = await jobHistoryService.GetByIdAsync(testJobHistory.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameJobHistory()
        {
            await jobHistoryService.UpdateAsync(testJobHistory.Id, testJobHistory);
            mockJobHistoryRepo.Verify(r => r.UpdateAsync(testJobHistory), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => jobHistoryService.UpdateAsync(testId, testJobHistory));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await jobHistoryService.RemoveByIdAsync(testJobHistory.Id);
            mockJobHistoryRepo.Verify(r => r.DeleteByIdAsync(testJobHistory.Id), Times.Once());
        }
    }
}
