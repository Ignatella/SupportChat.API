using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using IS.Data.Models;
using IS.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;

        public LoginController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto user) //ToDo: implement.
        {
            var userToBeCreated = mapper.Map<AppUser>(user);

            var result = await userManager.CreateAsync(userToBeCreated);

            result = userManager.AddClaimsAsync(userToBeCreated, new Claim[]
            {
                new Claim(JwtClaimTypes.NickName, user.KnownAs)
            }).Result;

            if (!result.Succeeded)
            {
                return BadRequest("An error occured while creating user. Try again.");
            }

            return NoContent();
        }
    }
}
