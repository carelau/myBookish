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
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

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

        public async Task<SignInResult> LogInUser(SignInModel signInModel)
        {

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);

            return result;
        }

        public async Task LogOutUser()
        {

            await _signInManager.SignOutAsync();

        }
    }
}