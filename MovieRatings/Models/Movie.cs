using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class Movie
    {
        public Movie()
        {
            MovieGenres = new HashSet<MovieGenre>();
            MovieActors = new HashSet<MovieActor>();
            MovieDirectors = new HashSet<MovieDirector>();
            MovieMediaHouses = new HashSet<MovieMediaHouse>();
            //Rating = new Rating();
        }
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name ="Release Date")]
        public string ReleaseDate { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"\d{1,20}(\.\d{1,2})?", ErrorMessage ="Price not valid")]
        public double Price { get; set; }
        [Display(Name ="Image")]
        public string ImgThumbnailUrl { get; set; }
        //public virtual Rating Rating { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
        public virtual ICollection<MovieMediaHouse> MovieMediaHouses { get; set; }
    }
}
