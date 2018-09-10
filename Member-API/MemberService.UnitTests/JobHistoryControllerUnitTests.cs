using FluentAssertions;
using MemberService.Controllers.ProfileControllers;
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
    public class JobHistoryControllerUnitTests
    {
        private readonly JobHistory testJobHistory = (JobHistory)ModelFactory.Creator<JobHistory>();
        private readonly JobHistoryDTO testJobHistoryDTO = (JobHistoryDTO)ModelFactory.Creator<JobHistoryDTO>();
        private readonly JobHistoryController jobHistoryController;

        public JobHistoryControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<JobHistoryDTO, JobHistory>>();

            mockService.Setup(srv => srv.AddAsync(testJobHistory))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult<JobHistoryDTO>(testJobHistoryDTO));

            mockService.Setup(srv => srv.UpdateAsync(testJobHistory.Id, testJobHistory))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testJobHistory.Id))
               .Returns(Task.CompletedTask);

            jobHistoryController = new JobHistoryController(mockService.Object);
        }

        [Fact]
        public async Task CreateClasses_Should_ReturnOk()
        {
            var result = await jobHistoryController.CreateJobHistory(testJobHistory);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindClass_Should_ReturnClassDTO_InJson()
        {
            var result = await jobHistoryController.FindJobHistory(testJobHistoryDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testJobHistoryDTO);
        }

        [Fact]
        public async Task UpdateClass_Should_ReturnOk()
        {
            var result = await jobHistoryController.UpdateJobHistory(testJobHistory.Id, testJobHistory);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteClass_Should_ReturnOk()
        {
            var result = await jobHistoryController.DeleteJobHistory(testJobHistory.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
