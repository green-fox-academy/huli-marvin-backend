using FluentAssertions;
using MemberService.Controllers.ProfileControllers;
using MemberService.Factories;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using MemberService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MemberService.UnitTests
{
    public class ProfileControllerUnitTests
    {
        private readonly Profile testProfile = (Profile)ModelFactory.Creator<Profile>();
        private readonly ProfileDTO testProfileDTO = (ProfileDTO)ModelFactory.Creator<ProfileDTO>();
        private readonly ProfileController profileController;

        public ProfileControllerUnitTests()
        {
            var mockService = new Mock<ICrudService<ProfileDTO, Profile>>();
            var mockStorageService = new Mock<IStorageService>();

            mockService.Setup(srv => srv.AddAsync(testProfile))
                .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.GetByIdAsync(testProfile.Id))
                .Returns(Task.FromResult<ProfileDTO>(testProfileDTO));

            mockService.Setup(srv => srv.UpdateAsync(testProfile.Id, testProfile))
               .Returns(Task.CompletedTask);

            mockService.Setup(srv => srv.RemoveByIdAsync(testProfile.Id))
               .Returns(Task.CompletedTask);

            var mockImage = new Mock<IFormFile>();
            mockStorageService.Setup(storageService => storageService.UploadPicture(mockImage.Object, testProfile.Id))
                .Returns(Task.FromResult<string>("images/" + testProfile.Id));

            profileController = new ProfileController(mockService.Object, mockStorageService.Object);
        }

        [Fact]
        public async Task CreateProfiles_Should_ReturnOk()
        {
            var result = await profileController.CreateProfile(testProfile);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task FindProfile_Should_ReturnProfileDTO_InJson()
        {
            var result = await profileController.FindProfile(testProfileDTO.Id);
            var jsonResult = result as JsonResult;

            jsonResult.Value.ShouldBeEquivalentTo(testProfileDTO);
        }

        [Fact]
        public async Task UpdateProfile_Should_ReturnOk()
        {
            var mockImage = new Mock<IFormFile>();
            var result = await profileController.UpdateProfile(testProfile.Id, testProfile);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteProfile_Should_ReturnOk()
        {
            var result = await profileController.DeleteProfile(testProfile.Id);
            var okResult = result as OkResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }
    }
}
