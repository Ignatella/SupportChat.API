using IdentityServer4;
using IS.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace IS.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly IdentityServerTools _tools;

        public AnonymousController(IdentityServerTools tools)
        {
            _tools = tools;
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            var token = _tools.IssueClientJwtAsync(
                clientId: "Anonymous",
                lifetime: 2678400, // 31 days
                scopes: new[] { "SignalR" },
                additionalClaims: new[] {
                    new Claim("sub", Guid.NewGuid().ToString()),
                    new Claim("anon", "true"),
                    new Claim("role", IdentityRoles.AnonymousUser.ToString()) }
                );

            return Ok(new
            {
                token
            });
        }
    }
}
