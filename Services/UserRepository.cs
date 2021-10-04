using System;
using System.Threading.Tasks;
using ZgloszeniaIT.Models;
using ZgloszeniaIT.Data;
using Microsoft.AspNetCore.Identity;

namespace ZgloszeniaIT.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(Guid Id)
        {
            return await _userManager.FindByIdAsync(Id.ToString());
        }

        public async Task<ApplicationUser> GetApplicationUserByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user;
        }
    }
}
