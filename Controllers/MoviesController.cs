using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Movies.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _db.Movies.Add(new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                Language = model.Language,
                ReleaseDate = model.ReleaseDate,
                Summary = model.Summary,
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if(movie == null)
                return NotFound();

            return View(new MovieViewModel()
            {
                Title = movie.Title,
                Director = movie.Director,
                Language = movie.Language,
                ReleaseDate = movie.ReleaseDate,
                Summary = movie.Summary
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieViewModel model)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            movie.Title = model.Title;
            movie.Director = model.Director;
            movie.Language = model.Language;
            movie.ReleaseDate = model.ReleaseDate;
            movie.Summary = model.Summary;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
