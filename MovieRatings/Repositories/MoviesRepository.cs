using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRatings.Common.Repositories;
using MovieRatings.Models;

namespace MovieRatings.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly AppDbContext appDbContext;

        public MoviesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return appDbContext.Movies.ToList();
        }

        public Movie GetMovieById(int movieId)
        {
            return appDbContext.Movies.FirstOrDefault(m => m.MovieId == movieId);
        }
    }
}
