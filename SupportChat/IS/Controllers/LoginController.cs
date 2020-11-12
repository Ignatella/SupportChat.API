using System;
using System.Threading.Tasks;
using IS.Data.Models;
using IS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get() //tmp method for dev.
        {
            Console.WriteLine(User.Identity.Name); 
            return Ok("hello");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username.ToUpper());

            if (user == null)
                return BadRequest("Check your credentials.");


            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
            if (result.Succeeded)
            {
                return NoContent();
            }
            
            return BadRequest("Check your credentials.");
        }
    }
}
