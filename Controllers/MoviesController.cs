using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel model)
        {
            return View();
        }
    }
}
