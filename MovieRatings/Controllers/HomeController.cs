using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieRatings.Common.Repositories;
using MovieRatings.Models;

namespace MovieRatings.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMoviesRepository movieRepository;

        public HomeController(IMoviesRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            var movies = movieRepository.GetAllMovies().OrderBy(m => m.Title);

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var movie = movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
