using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRatings.Models;
using Newtonsoft.Json;

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
            return await _context.Movies.
                Include( p => p.MovieGenres)
                .Include(p => p.MovieDirectors)
                .Include(p => p.MovieActors)
                .Include(p => p.MovieMediaHouses)
                .ToListAsync();
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
            if (movie != null)
            {
                foreach (var item in query.Genres)
                {
                    var genre = await _context.Genres.FindAsync(item.GenreId);
                    if (genre == null)
                        continue;
                    else
                    movie.MovieGenres.Add(new MovieGenre { Genre = genre });
                }
                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{movieId}/genre")]
        public async Task<ActionResult<Movie>> DeleteGenre(int movieId, Genre movieGenre)
        {
            var movie = await _context.Movies.Include(m => m.MovieGenres).SingleOrDefaultAsync(m => m.MovieId == movieId);
            if (movie != null)
            {

                var genre = await _context.Genres.FindAsync(movieGenre.GenreId);
                if (genre != null)
                    movie.MovieGenres.Remove(movie.MovieGenres.Where(mg => mg.GenreId == genre.GenreId).FirstOrDefault());

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPut("{movieId}/actor")]
        public async Task<ActionResult<Movie>> AddActor(int movieId, Query query)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie != null)
            {
                foreach (var item in query.Actors)
                {
                    var actor = await _context.Actors.FindAsync(item.ActorId);
                    if (actor == null)
                        continue;
                    else
                        movie.MovieActors.Add(new MovieActor { Actor = actor, ActOnDate = item.ActedOnDate });
                }


                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{movieId}/actor")]
        public async Task<ActionResult<Movie>> DeleteActor(int movieId, Actor movieActor)
        {
            var movie = await _context.Movies.Include(m => m.MovieActors).SingleOrDefaultAsync(m => m.MovieId == movieId);
            if (movie != null)
            {

                var actor = await _context.Actors.FindAsync(movieActor.ActorId);
                if (actor != null)
                    movie.MovieActors.Remove(movie.MovieActors.Where(ma => ma.ActorId == actor.ActorId).FirstOrDefault());

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPut("{movieId}/director")]
        public async Task<ActionResult<Movie>> AddDirector(int movieId, Query query)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie != null)
            {
                foreach (var item in query.Directors)
                {
                    var director = await _context.Directors.FindAsync(item.DirectorId);

                    if (director == null)
                        continue;
                    else
                    movie.MovieDirectors.Add(new MovieDirector { Director = director, DirectedOnDate = item.DirectedOnDate });

                }
                
                _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{movieId}/director")]
        public async Task<ActionResult<Movie>> DeleteDirector(int movieId, Director movieDirector)
        {
            var movie = await _context.Movies.Include(m => m.MovieDirectors).SingleOrDefaultAsync(m => m.MovieId == movieId);
            if (movie != null)
            {
                var director = await _context.Directors.FindAsync(movieDirector.DirectorId);

                if (director != null)
                    movie.MovieDirectors.Remove(movie.MovieDirectors.Where(md => md.DirectorId == director.DirectorId).FirstOrDefault());



                await _context.SaveChangesAsync();

            }
            else
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPut("{movieId}/mediaHouse")]
        public async Task<ActionResult<Movie>> AddMediaHouse(int movieId, Query query)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie != null)
            {
                foreach(var item in query.MediaHouses)
                {
            var mediaHouse = await _context.MediaHouses.FindAsync(item.MediaHouseId);
                    if (mediaHouse == null)
                        continue;
                    else
                movie.MovieMediaHouses.Add(new MovieMediaHouse { MediaHouse = mediaHouse });
                }
                 _context.Entry(movie).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{movieId}/mediaHouse")]
        public async Task<ActionResult<Movie>> DeleteMediaHouse(int movieId, MediaHouse movieMediaHouse)
        {
            var movie = await _context.Movies.Include(m => m.MovieMediaHouses).SingleOrDefaultAsync(m => m.MovieId == movieId);
            if (movie != null)
            {
                    var mediaHouse = await _context.MediaHouses.FindAsync(movieMediaHouse.MediaHouseId);
                    if (mediaHouse != null)
                        movie.MovieMediaHouses.Remove(movie.MovieMediaHouses.Where(mmh=>mmh.MediaHouseId == mediaHouse.MediaHouseId).FirstOrDefault());

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return movie;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        public class Query
        {
            public List<Actor> Actors { get; set; }
            public List<Director> Directors { get; set; }
            public List<Genre> Genres { get; set; }
            public List<MediaHouse> MediaHouses { get; set; }
        }
               
        public class Actor
        {
            public int ActorId { get; set; }
            public int ActedOnDate { get; set; }
        }

        public class Director {
            public int DirectorId { get; set; }
            public int DirectedOnDate { get; set; }
        }
        public class Genre
        {
            public int GenreId { get; set; }
        }
        public class MediaHouse
        {
            public int MediaHouseId { get; set; }
        }

    }
}
