using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IS.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string UserAgent { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

        public ICollection<AppUserAnonymousId> UserAnonymousIds { get; set; }
    }
}   
