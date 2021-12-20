using Microsoft.AspNetCore.Mvc;
using mybookish.Models;
using mybookish.Repository;

namespace mybookish.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorRepository _authorRepository;
        public AuthorController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public IActionResult AddNewAuthor(bool isSuccess = false)
        {
            ViewBag.isSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAuthor(AuthorModel authorModel)
        {

            if (ModelState.IsValid)
            {
                int id = _authorRepository.AddAuthor(authorModel);

                if (id > 0)
                {
                    return RedirectToAction("AddNewAuthor", "Author", new { isSuccess = true });
                }
            }
            return View();

        }
    }
}
