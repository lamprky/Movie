using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        public virtual IList<Translation> Translations { get; set; }

        public IList<MovieGenres> MovieGenres { get; set; }

    }
}
