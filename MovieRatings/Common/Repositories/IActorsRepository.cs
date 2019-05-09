using MovieRatings.Models;
using System.Collections.Generic;

namespace MovieRatings.Common.Repositories
{
    public interface IActorsRepository
    {
        Actor GetActorById(int actorId);
        IEnumerable<Actor> GetAllActors();
    }
}
