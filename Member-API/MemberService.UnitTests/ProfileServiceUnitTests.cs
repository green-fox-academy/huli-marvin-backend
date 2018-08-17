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
    public class ProfileServiceUnitTests
    {
        private readonly Profile testProfile = (Profile)ModelFactory.Creator<Profile>();
        private readonly ProfileDTO testProfileDTO = (ProfileDTO)ModelFactory.Creator<ProfileDTO>();
        private readonly ProfileService profileService;
        private readonly IEnumerable<Profile> profiles;
        private readonly Mock<ICrudRepository<Profile>> mockProfileRepo = new Mock<ICrudRepository<Profile>>();

        public ProfileServiceUnitTests()
        {
            profiles = new List<Profile>() { testProfile };

            mockProfileRepo.Setup(r => r.SelectAllAsync(new QueryCollection()))
                .Returns(Task.FromResult(profiles));

            mockProfileRepo.Setup(r => r.SelectByIdAsync(testProfile.Id))
                .Returns(Task.FromResult(testProfile));

            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<ProfileDTO>(It.IsAny<Profile>())).Returns(testProfileDTO);

            profileService = new ProfileService(mockProfileRepo.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCourseDTO()
        {
            var expected = new List<ProfileDTO>() { testProfileDTO };
            var result = await profileService.GetAllAsync(new QueryCollection());
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_BadRequestException()
        {
            var exception = await Record.ExceptionAsync(() => profileService.GetAllAsync(null));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task AddAsync_Should_CallInsertAsync_WithSameCourse()
        {
            await profileService.AddAsync(testProfile);
            mockProfileRepo.Verify(r => r.InsertAsync(testProfile), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCourseDTO()
        {
            var expected = testProfileDTO;
            var result = await profileService.GetByIdAsync(testProfile.Id);
            result.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_Should_CallUpdateAsync_WithSameCourse()
        {
            await profileService.UpdateAsync(testProfile.Id, testProfile);
            mockProfileRepo.Verify(r => r.UpdateAsync(testProfile), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_BadRequestException()
        {
            const long testId = 9999;
            var exception = await Record.ExceptionAsync(() => profileService.UpdateAsync(testId, testProfile));
            Assert.IsType<BadRequestException>(exception);
        }

        [Fact]
        public async Task RemoveByIdAsync_Should_CallDeleteByIdAsync_WithSameId()
        {
            await profileService.RemoveByIdAsync(testProfile.Id);
            mockProfileRepo.Verify(r => r.DeleteByIdAsync(testProfile.Id), Times.Once());
        }
    }
}
