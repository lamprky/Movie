using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("MovieGenres")]
    public class MovieGenres
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid MovieID { get; set; }
        public Movie Movie { get; set; }


        [Required]
        public Guid GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
