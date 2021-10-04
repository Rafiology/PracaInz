using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZgloszeniaIT.Models;
using ZgloszeniaIT.Data;

namespace ZgloszeniaIT.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetApplicationUserAsync(Guid Id);
        Task<ApplicationUser> GetApplicationUserByName(string name);
    }
}
