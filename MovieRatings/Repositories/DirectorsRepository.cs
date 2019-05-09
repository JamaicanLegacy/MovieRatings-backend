using MovieRatings.Common.Repositories;
using MovieRatings.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieRatings.Repositories
{
    public class DirectorsRepository : IDirectorsRepository
    {
        private readonly AppDbContext appDbContext;

        public DirectorsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IEnumerable<Director> GetAllDirectors()
        {
            return appDbContext.Directors;
        }

        public Director GetDirectorById(int directorId)
        {
            return appDbContext.Directors.FirstOrDefault(d => d.DirectorId == directorId);
        }
    }
}
