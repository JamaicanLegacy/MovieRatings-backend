using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRatings.Models;

namespace MovieRatings.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        // PUT api/Movies/5
        [HttpPut("{movieId}/genre")]
        public async Task<ActionResult<Movie>> AddGenre(int movieId, Query query)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var genre = await _context.Genres.FindAsync(query.GenreId);
            if(movie != null)
            {
                movie.MovieGenres.Add(new MovieGenre { Genre = genre});

                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }
        // PUT api/Movies/5
        [HttpPut("{movieId}/actor")]
        public async Task<ActionResult<Movie>> AddActor(int movieId, ActorRequest query)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var actor = await _context.Actors.FindAsync(query.ActorId);
            if (movie != null)
            {
                movie.MovieActors.Add(new MovieActor { Actor = actor, ActOnDate = query.ActedOnDate });

                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("{movieId}/director")]
        public async Task<ActionResult<Movie>> AddDirector(int movieId, DirectorRequest query)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var director = await _context.Directors.FindAsync(query.DirectorId);
            if(movie != null)
            {
                movie.MovieDirectors.Add(new MovieDirector { Director = director, DirectedOnDate = query.DirectedOnDate });

                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("{movieId}/mediaHouse")]
        public async Task<ActionResult<Movie>> AddMediaHouse(int movieId, Query query)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var mediaHouse = await _context.MediaHouses.FindAsync(query.MediaHouseId);
            if(movie != null)
            {
                movie.MovieMediaHouses.Add(new MovieMediaHouse { MediaHouse = mediaHouse });

                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent(); 
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        public class Query
        {
            public int GenreId { get; set; }
            public int MediaHouseId { get; set; }
        }

        public class ActorRequest
        {
            public int ActorId { get; set; }
            public int ActedOnDate { get; set; }
        }

        public class DirectorRequest
        {
            public int DirectorId { get; set; }
            public int DirectedOnDate { get; set; }
        }
    }
}
