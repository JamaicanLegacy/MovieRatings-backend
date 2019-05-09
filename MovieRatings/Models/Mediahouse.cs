using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class MediaHouse
    {
        public MediaHouse()
        {
            MovieMediaHouses = new HashSet<MovieMediaHouse>();
        }
        public int MediaHouseId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<MovieMediaHouse> MovieMediaHouses { get; set; }
    }
}
