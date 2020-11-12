using Microsoft.AspNetCore.Identity;


namespace IS.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string UserAgent { get; set; }
    }
}   
