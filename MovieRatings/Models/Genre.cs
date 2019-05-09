using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRatings.Models
{
    public class Genre
    {
        public Genre()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }
        public int GenreId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

    }
}
