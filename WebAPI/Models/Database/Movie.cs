using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        public IList<Translation> Translations { get; set; }

        public IList<MovieGenres> MovieGenres { get; set; }

        public IList<MovieContributors> MovieContributors { get; set; }

    }
}
