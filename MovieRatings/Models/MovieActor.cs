using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public int ActOnDate { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
