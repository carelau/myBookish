using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mybookish.Database;
using mybookish.Models;
using System.Linq;
using System.Threading.Tasks;

namespace mybookish.Repository
{
    public class AccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddUser(RegisterModel registerModel)
        {
            var user = new IdentityUser()
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            return result;
        }

    }
}