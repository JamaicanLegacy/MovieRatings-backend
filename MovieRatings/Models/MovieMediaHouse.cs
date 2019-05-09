using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class MovieMediaHouse
    {
        public int MovieId { get; set; }
        public int MediaHouseId { get; set; }
        public Movie Movie { get; set; }
        public MediaHouse MediaHouse { get; set; }
    }
}
