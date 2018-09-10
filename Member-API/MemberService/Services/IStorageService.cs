using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MemberService.Services
{
    public interface IStorageService
    {
        Task<string> UploadPicture(IFormFile image, long profileId);
    }
}
