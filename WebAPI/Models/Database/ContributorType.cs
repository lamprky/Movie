using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("ContributorType")]
    public class ContributorType
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        public virtual IList<Translation> Translations { get; set; } = new List<Translation>();

        public IList<ContributorContributorTypes> ContributorContributorTypes { get; set; }
    }
}
