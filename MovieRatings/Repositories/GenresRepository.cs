using MovieRatings.Common.Repositories;
using MovieRatings.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieRatings.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly AppDbContext appDbContext;

        public GenresRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return appDbContext.Genres;
        }

        public Genre GetGenreById(int genreId)
        {
            return appDbContext.Genres.FirstOrDefault(g => g.GenreId == genreId);
        }
    }
}
