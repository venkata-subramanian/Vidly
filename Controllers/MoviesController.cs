using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();    
        }
        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("MoviesList");

            return View("ReadonlyMoviesList");
        }

        public ActionResult Details(int Id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult MovieForm()
        {
            var genres = _context.Genres.ToList();

            var movieFormViewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View(movieFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movies movie)
        {
            if(!ModelState.IsValid)
            {
                return View("MovieForm", new MovieFormViewModel(movie) { Genres = _context.Genres.ToList() });
            }

            //New Movie
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now; 
                _context.Movies.Add(movie);
            }
                
            //Existing Movie
            else
            {
                var existingMovie = _context.Movies.Single(m => m.Id == movie.Id);

                existingMovie.Name = movie.Name;
                existingMovie.GenreId = movie.GenreId;
                existingMovie.NumberInStock = movie.NumberInStock;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.DateAdded = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
            
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (movie == null)
                return HttpNotFound();

            var moviesViewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", moviesViewModel);
        }
    }
}