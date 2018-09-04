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
    public class ClassServiceUnitTests
    {
        private readonly Class testClass = (Class)ModelFactory.Creator<Class>();
        private readonly ClassDTO testClassDTO = (ClassDTO)ModelFactory.Creator<ClassDTO>();
        private readonly ClassService classService;
        private readonly IEnumerable<Class> classes;
        private readonly Mock<ICrudRepository<Class>> mockClassRepo = new Mock<ICrudRepository<Class>>();

        public ClassServiceUnitTests()
        {
            classes = new List<Class>() { testClass };

            mockClassRepo.Setup(r =>  r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(classes));

            mockClassRepo.Setup(r => r.SelectByIdAsync(testClass.Id))
                .Returns(Task.FromResult(testClass));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<ClassDTO>(It.IsAny<Class>())).Returns(testClassDTO);
            
            classService = new ClassService(mockClassRepo.Object, mockMapper.Object);
        }
        
        [Fact]
        public async Task GetAllAsync_Should_ReturnClassDTO()
        {
            var result = await classService.GetAllAsync(new QueryCollection());
            var expected = new List<ClassDTO>() { testClassDTO };
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => classService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameClass()
        {
            await classService.AddAsync(testClass);
            mockClassRepo.Verify(r => r.InsertAsync(testClass), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnClassDTO()
        {  
            var result = await classService.GetByIdAsync(testClass.Id);
            var expected = testClassDTO;
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameClass()
        {
            await classService.UpdateAsync(testClass.Id, testClass);
            mockClassRepo.Verify(r => r.UpdateAsync(testClass), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => classService.UpdateAsync(testId, testClass));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await classService.RemoveByIdAsync(testClass.Id);
            mockClassRepo.Verify(r => r.DeleteByIdAsync(testClass.Id), Times.Once());
        }
    }
}
