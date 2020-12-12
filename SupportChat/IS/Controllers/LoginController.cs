using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServerHost.Quickstart.UI;
using IS.Data.Models;
using IS.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IS.Controllers
{
    [SecurityHeaders]
    [Route("identity/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEventService _events;

        public LoginController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEventService envents)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _events = envents;
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username.ToUpper());

            if (user == null)
                return BadRequest("Check your credentials.");


            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
            if (result.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));
                return NoContent();
            }

            return BadRequest("tmp error message");
        }
    }
}
