using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS.Dtos
{
    public class RegisterDto
    {
        public string UserAgent { get; set; } // use regexp to store parts if UserAgent for easier statistic

        public string KnownAs { get; set; }

        public string Email { get; set; } //use regexp
    }
}
