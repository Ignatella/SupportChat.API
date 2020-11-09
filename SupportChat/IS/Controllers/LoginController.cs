using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = userManager.Users.First(u => u.Id == "d34017a8-337e-4f95-88de-f6b9739428b3");

            await signInManager.SignInAsync(user, true);

            //var result = await signInManager.PasswordSignInAsync("alice", "Pass123$", false, false);
            return Ok("nothing to return here");
        }
    }
}
