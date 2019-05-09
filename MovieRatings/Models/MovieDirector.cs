using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class MovieDirector
    {
        public int MovieId { get; set; }
        public int DirectorId { get; set; }
        public int DirectedOnDate { get; set; }
        public Movie Movie { get; set; }
        public Director Director { get; set; }
    }
}
