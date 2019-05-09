using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class Director
    {
        public Director()
        {
            MovieDirectors = new HashSet<MovieDirector>();
        }
        public int DirectorId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date career started")]
        public string IndustryStartDate { get; set; }
        [Display(Name = "Date of Retirment")]
        public string RetirementDate { get; set; }
        public bool? Active { get; set; }
        public string Gender { get; set; }
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }

    }
}
