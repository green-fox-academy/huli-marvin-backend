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
    public class CohortServiceTests
    {
        private readonly Cohort testCohort = (Cohort)ModelFactory.Creator<Cohort>();
        private readonly CohortDTO testCohortDTO = (CohortDTO)ModelFactory.Creator<CohortDTO>();
        private readonly CohortService cohortService;
        private readonly IEnumerable<Cohort> cohorts;
        private readonly Mock<ICrudRepository<Cohort>> mockCohortRepo = new Mock<ICrudRepository<Cohort>>();
        
        public CohortServiceTests()
        {
            cohorts = new List<Cohort>() { testCohort };

            mockCohortRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(cohorts));

            mockCohortRepo.Setup(r => r.SelectByIdAsync(testCohort.Id))
                .Returns(Task.FromResult(testCohort));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<CohortDTO>(It.IsAny<Cohort>())).Returns(testCohortDTO);

            cohortService = new CohortService(mockCohortRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCohortDTO()
        {
            var expected = new List<CohortDTO>() { testCohortDTO };
            var result = await cohortService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => cohortService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameCohort()
        {
            await cohortService.AddAsync(testCohort);
            mockCohortRepo.Verify(r => r.InsertAsync(testCohort), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCohortDTO()
        {
            var expected = testCohortDTO;
            var result = await cohortService.GetByIdAsync(testCohort.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameCohort()
        {
            await cohortService.UpdateAsync(testCohort.Id, testCohort);
            mockCohortRepo.Verify(r => r.UpdateAsync(testCohort), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => cohortService.UpdateAsync(testId, testCohort));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync()
        {
            await cohortService.RemoveByIdAsync(testCohort.Id);
            mockCohortRepo.Verify(r => r.DeleteByIdAsync(testCohort.Id), Times.Once());
        }
    }
}
