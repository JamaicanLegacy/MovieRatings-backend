using MovieRatings.Models;
using System.Collections.Generic;

namespace MovieRatings.Common.Repositories
{
    public interface IDirectorsRepository
    {
        Director GetDirectorById(int directorId);
        IEnumerable<Director> GetAllDirectors();        
    }
}
