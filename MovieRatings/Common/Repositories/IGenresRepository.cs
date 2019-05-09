using MovieRatings.Models;
using System.Collections.Generic;

namespace MovieRatings.Common.Repositories
{
    public interface IGenresRepository
    {
        Genre GetGenreById(int GenreId);
        IEnumerable<Genre> GetAllGenres();        
    }
}
