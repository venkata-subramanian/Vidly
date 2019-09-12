using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalsDTO newRentalsDTO)
        {
            if (newRentalsDTO.movieIds.Count == 0)
                return BadRequest("Movies not supplied");

            var customer = _context.Customers.Single(c => c.Id == newRentalsDTO.customerId);

            var movieList = _context.Movies.Where(
                m => newRentalsDTO.movieIds.Contains(m.Id)).ToList();

            foreach (Movies movie in movieList)
            {
                if (movie.NumberAvailable <= 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                _context.Rentals.Add(new Rentals()
                {
                    Customers = customer,
                    Movies = movie,
                    DateRented = DateTime.Now
                });

            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
