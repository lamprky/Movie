using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("MovieContributors")]
    public class MovieContributors
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid MovieID { get; set; }
        public Movie Movie { get; set; }


        [Required]
        public Guid ContributorID { get; set; }
        public Contributor Contributor { get; set; }
    }
}
