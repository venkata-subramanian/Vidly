using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MovieAPIController : ApiController
    {
        private ApplicationDbContext _context;

        public MovieAPIController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/MovieAPI/
        public IHttpActionResult GetMovies()
        {
            var moviesDTO = _context.Movies
                               .Include(g => g.Genre)
                               .Where(m => m.NumberAvailable > 0)
                               .ToList()
                               .Select(Mapper.Map<Movies, MovieDTO>);

            return Ok(moviesDTO);
        }

        //GET /api/MovieAPI/1
        public IHttpActionResult GetMovie(int Id)
        {
            var movie = _context.Movies
                            .Include(g => g.Genre)
                            .SingleOrDefault(m => m.Id == Id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movies, MovieDTO>(movie));
        }

        //POST /api/MovieAPI/
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDTO, Movies>(movieDTO);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDTO.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDTO);
        }

        //PUT /api/MovieAPI/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int Id, MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var existingMovie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (existingMovie == null)
                return NotFound();

            Mapper.Map(movieDTO, existingMovie);
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/MovieAPI/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}
