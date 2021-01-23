using ChilliApp.Models;
using ChilliApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ChilliApp.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Movie
        public ActionResult Index()
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .ToList();

            return View(movie);
        }

        public ActionResult Detail(int? id) 
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public ActionResult Random() 
        {
            var movie = new Movie()
            {
                Id = 1,
                Name = "Sing"
            };

            var customers = new List<Customer>
            {
                new Customer{ Id = 1, Name = "Sese"},
                new Customer{ Id = 2, Name = "Tope"},
                new Customer{ Id = 1, Name = "Bayo"}

            };

            var random = new RandomMovieViewModel 
            {
                Movie = movie,
                Customer = customers
            };
            return View(random);
        }
    }
}