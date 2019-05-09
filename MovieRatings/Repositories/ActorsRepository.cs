using System.Collections.Generic;
using System.Linq;
using MovieRatings.Common.Repositories;
using MovieRatings.Models;

namespace MovieRatings.Repositories
{
    public class ActorsRepository : IActorsRepository
    {
        private readonly AppDbContext appDbContext;

        public ActorsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Actor GetActorById(int actorId)
        {
            return appDbContext.Actors.FirstOrDefault(a => a.ActorId == actorId);
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return appDbContext.Actors;
        }

    }
}
