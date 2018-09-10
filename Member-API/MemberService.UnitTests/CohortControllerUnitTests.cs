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
    public class CohortControllerUnitTests
    {
        private Cohort testCohort = (Cohort)ModelFactory.Creator<Cohort>();
        private CohortDTO testCohortDTO = (CohortDTO)ModelFactory.Creator<CohortDTO>();
        private CohortController cohortController;

        public CohortControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<CohortDTO, Cohort>>();

            mockService.Setup(srv => srv.AddAsync(testCohort))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(testCohort.Id))
                .Returns(Task.FromResult(testCohortDTO));

            mockService.Setup(srv => srv.UpdateAsync(testCohort.Id, testCohort))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testCohort.Id))
               .Returns(Task.CompletedTask);

            cohortController = new CohortController(mockService.Object);
        }

        [Fact]
        public async Task CreateCohorts_Should_ReturnOk()
        {
            var result = await cohortController.CreateCohort(testCohort);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindCohort_Should_ReturnCohortDTO_InJson()
        {
            var result = await cohortController.FindCohort(testCohortDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testCohortDTO);
        }

        [Fact]
        public async Task UpdateCohort_Should_ReturnOk()
        {
            var result = await cohortController.UpdateCohort(testCohort.Id, testCohort);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCohort_Should_ReturnOk()
        {
            var result = await cohortController.DeleteCohort(testCohort.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
