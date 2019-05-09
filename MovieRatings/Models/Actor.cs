using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRatings.Models
{
    public class Actor
    {
        public Actor()
        {
            MovieActors = new HashSet<MovieActor>();
        }
        public int ActorId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date Since Active")]
        public string IndustryStartDate { get; set; }
        [Display(Name ="Date of Retirment")]
        public string RetirementDate { get; set; }
        public bool? Active { get; set; }
        public string Gender { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}
