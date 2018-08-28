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
    public class DepartmentControllerUnitTests
    {
        private readonly Department testDepartment = (Department)ModelFactory.Creator<Department>();
        private readonly DepartmentDTO testDepartmentDTO = (DepartmentDTO)ModelFactory.Creator<DepartmentDTO>();
        private readonly DepartmentController departmentController;

        public DepartmentControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<DepartmentDTO, Department>>();

            mockService.Setup(srv => srv.AddAsync(testDepartment))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(testDepartmentDTO));

            mockService.Setup(srv => srv.UpdateAsync(testDepartment.Id, testDepartment))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testDepartment.Id))
               .Returns(Task.CompletedTask);

            departmentController = new DepartmentController(mockService.Object);
        }

        [Fact]
        public async Task CreateDepartments_Should_ReturnOk()
        {
            var result = await departmentController.CreateDepartment(testDepartment);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindDepartment_Should_ReturnDepartmentDTO_InJson()
        {
            var result = await departmentController.FindDepartment(testDepartmentDTO.Id);
            var jsonResult = result as JsonResult;
            var expected = testDepartmentDTO;
            jsonResult.Value.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateDepartment_Should_ReturnOk()
        {
            var result = await departmentController.UpdateDepartment(testDepartment.Id, testDepartment);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteClass_Should_ReturnOk()
        {
            var result = await departmentController.DeleteDepartment(testDepartment.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
