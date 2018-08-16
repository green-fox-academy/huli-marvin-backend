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
    public class DepartmentServiceTests
    {
        private readonly Department testDepartment = (Department)ModelFactory.Creator<Department>();
        private readonly DepartmentDTO testDepartmentDTO = (DepartmentDTO)ModelFactory.Creator<DepartmentDTO>();
        private readonly DepartmentService departmentService;
        private readonly IEnumerable<Department> departments;
        private readonly Mock<ICrudRepository<Department>> mockDepartmentRepo = new Mock<ICrudRepository<Department>>();

        public DepartmentServiceTests()
        {
            departments = new List<Department>() { testDepartment };

            mockDepartmentRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(departments));

            mockDepartmentRepo.Setup(r => r.SelectByIdAsync(testDepartment.Id))
                .Returns(Task.FromResult(testDepartment));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<DepartmentDTO>(It.IsAny<Department>())).Returns(testDepartmentDTO);

            departmentService = new DepartmentService(mockDepartmentRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnDepartmentDTO()
        {
            var expected = new List<DepartmentDTO>() { testDepartmentDTO };
            var result = await departmentService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => departmentService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameDepartment()
        {
            await departmentService.AddAsync(testDepartment);
            mockDepartmentRepo.Verify(r => r.InsertAsync(testDepartment), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnDepartmentDTO()
        {
            var expected = testDepartmentDTO;
            var result = await departmentService.GetByIdAsync(testDepartment.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameDepartment()
        {
            await departmentService.UpdateAsync(testDepartment.Id, testDepartment);
            mockDepartmentRepo.Verify(r => r.UpdateAsync(testDepartment), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => departmentService.UpdateAsync(testId, testDepartment));
            Assert.NotNull(exception);
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await departmentService.RemoveByIdAsync(testDepartment.Id);
            mockDepartmentRepo.Verify(r => r.DeleteByIdAsync(testDepartment.Id), Times.Once());
        }
    }
}
