using MemberService.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MemberService.Controllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [Route("auth")]
        public IActionResult GetToken([FromQuery]string redirect)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            if (redirect != null)
            {
                if (authService.UserExists(email))
                {
                    return Redirect(authService.GetUrl(redirect));
                }

                return NotFound("User not found!");
            }

            return NotFound("Redirect URL not found!");
        }
    }
}
