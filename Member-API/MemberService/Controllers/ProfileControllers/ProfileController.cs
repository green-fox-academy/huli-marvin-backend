using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using MemberService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MemberService.Controllers.ProfileControllers
{
    [Route("v1")]
    public class ProfileController : Controller
    {
        private readonly ICrudService<ProfileDTO, Profile> profileService;
        private readonly IStorageService storageService;

        public ProfileController(ICrudService<ProfileDTO, Profile> profileService, IStorageService storageService)
        {
            this.profileService = profileService;
            this.storageService = storageService;
        }

        [HttpGet("profiles")]
        public async Task<IActionResult> ListProfiles()
        {
            var profiles = await profileService.GetAllAsync(Request.Query);
            return Json(profiles);
        }

        [HttpPost("profiles")]
        public async Task<IActionResult> CreateProfile([FromBody]Profile newProfile)
        {
            await profileService.AddAsync(newProfile);
            return Ok();
        }

        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> FindProfile(long profileId)
        {
            var profile = await profileService.GetByIdAsync(profileId);
            return Json(profile);
        }

        [HttpPut("profile/{profileId}")]
        public async Task<IActionResult> UpdateProfile(long profileId, [FromBody]Profile updateProfile)
        {
            await profileService.UpdateAsync(profileId, updateProfile);
            return Ok();
        }

        [HttpDelete("profile/{profileId}")]
        public async Task<IActionResult> DeleteProfile(long profileId)
        {
            await profileService.RemoveByIdAsync(profileId);
            return Ok();
        }

        [HttpPost("profile/upload")]
        public async Task<IActionResult> Upload([FromBody]Profile updateProfile, [FromForm]IFormFile image)
        {
            updateProfile.Picture = await storageService.UploadPicture(image, updateProfile.Id);
            await profileService.UpdateAsync(updateProfile.Id,updateProfile);
            return Ok();
        }
    }
}
