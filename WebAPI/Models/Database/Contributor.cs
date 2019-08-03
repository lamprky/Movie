using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("Contributor")]
    public class Contributor
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        public IList<Translation> Translations { get; set; } = new List<Translation>();

        public IList<MovieContributors> MovieContributors { get; set; } = new List<MovieContributors>();

        public IList<ContributorContributorTypes> ContributorContributorTypes { get; set; } = new List<ContributorContributorTypes>();
    }
}
