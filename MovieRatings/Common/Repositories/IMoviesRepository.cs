using MovieRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Common.Repositories
{
    public interface IMoviesRepository
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int movieId);
    }
}
