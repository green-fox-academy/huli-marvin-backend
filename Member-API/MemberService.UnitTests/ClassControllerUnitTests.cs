using FluentAssertions;
using MemberService.Controllers.EducationControllers;
using MemberService.Factories;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.UnitTests
{
    public class ClassControllerUnitTests
    {
        private readonly Class testClass = (Class)ModelFactory.Creator<Class>();
        private readonly ClassDTO testClassDTO = (ClassDTO)ModelFactory.Creator<ClassDTO>();
        private readonly ClassController classController;
        private readonly IEnumerable<ClassDTO> classDTOs;

        public ClassControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<ClassDTO, Class>>();
            classDTOs = new List<ClassDTO>() { testClassDTO };

            mockService.Setup(srv => srv.GetAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(classDTOs));

            mockService.Setup(srv => srv.AddAsync(testClass))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(testClassDTO));

            mockService.Setup(srv => srv.UpdateAsync(testClass.Id, testClass))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testClass.Id))
               .Returns(Task.CompletedTask);
            
            classController = new ClassController(mockService.Object);
        }

        [Fact]
        public async Task ListClasses_Should_ReturnClassDTOs_InJson()
        {
            var result = await classController.FindClass(testClassDTO.Id);
            var jsonResult = result as JsonResult;
            jsonResult.Value.ShouldBeEquivalentTo(testClassDTO);
        }

        [Fact]
        public async Task CreateClass_Should_ReturnOk()
        {
            var result = await classController.CreateClass(testClass);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindClass_Should_ReturnClassDTO_InJson()
        {
            var result = await classController.FindClass(testClassDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testClassDTO);
        }

        [Fact]
        public async Task UpdateClass_Should_ReturnOk()
        {
            var result = await classController.UpdateClass(testClass.Id, testClass);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteClass_Should_ReturnOk()
        {
            var result = await classController.DeleteClass(testClass.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
