using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mybookish.Models;
using mybookish.Repository;

namespace mybookish.Controllers
{
    public class AccountController : Controller
    {

        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountRepository.AddUser(registerModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(registerModel);
                }
                ModelState.Clear();
            }
            return View();

        }
    }
}
