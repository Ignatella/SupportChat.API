using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS.Data.Models
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public AppUser User { get; set; }

        public AppRole Role { get; set; }
    }
}
