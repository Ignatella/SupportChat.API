using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IS.Data;
using IS.Data.Models;
using IS.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Is.Data.Seed
{
    public static class SeedIdentityTables
    {
        public static async void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DataContext>();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();

                #region Drop Users and Roles
                var users = await userMgr.Users.ToListAsync();
                foreach (var user in users)
                {
                    await userMgr.DeleteAsync(user);
                }

                var roles = await roleManager.Roles.ToListAsync();
                foreach (var role in roles)
                {
                    await roleManager.DeleteAsync(role);
                }

                #endregion

                #region Seed Roles

                foreach (var item in Enum.GetValues(typeof(IdentityRoles)))//ToDo: implement in right way
                {
                    await roleManager.CreateAsync(new AppRole { Name = item.ToString() });
                }

                #endregion

                #region Seed Users
                
                var alice = userMgr.FindByNameAsync("alice").Result;
                if (alice == null)
                {
                    alice = new AppUser
                    {
                        UserName = "alice"
                    };
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;


                    var roleResult = await userMgr.AddToRolesAsync(alice,
                         Enum.GetValues(typeof(IdentityRoles)).Cast<IdentityRoles>().Select(r => r.ToString()));

                    if (!result.Succeeded || !roleResult.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("alice created");
                }
                else
                {
                    Console.WriteLine("alice already exists");
                }

                var bob = userMgr.FindByNameAsync("bob").Result;
                if (bob == null)
                {
                    bob = new AppUser
                    {
                        UserName = "bob"
                    };
                    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }).Result;

                    var roleResult = await userMgr.AddToRoleAsync(bob, IdentityRoles.Member.ToString());

                    if (!result.Succeeded || !roleResult.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("bob created");
                }
                else
                {
                    Console.WriteLine("bob already exists");
                }

                #endregion
            }
        }
    }
}
