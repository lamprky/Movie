using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("Translation")]
    public class Translation
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        //[Required]
        //public Guid EntityId { get; set; }

        //[Required]
        //public Guid EntityTypeId { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        public Language Language { get; set; }

        //public EntityType EntityType { get; set; }

        public ContributorType ContributorType { get; set; }

        public Contributor Contributor { get; set; }

        public Genre Genre { get; set; }

        public Movie Movie { get; set; }
    }
}
