using Microsoft.AspNetCore.Identity;
using System;

namespace ZgloszeniaIT.Models
{
    public class UserRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
