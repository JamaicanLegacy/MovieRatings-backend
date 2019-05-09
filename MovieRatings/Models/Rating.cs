using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class Rating
    {
        public Rating()
        {
            Movie = new Movie();
        }
        public int MovieId { get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public string Reason { get; set; }
        public double IMDBScore { get; set; }
        public double RottenTomatoesScore { get; set; }
        public virtual Movie Movie { get; set; }

    }
}
