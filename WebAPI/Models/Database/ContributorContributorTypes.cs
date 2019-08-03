using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("ContributorContributorTypes")]
    public class ContributorContributorTypes
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid ContributorID { get; set; }
        public Contributor Contributor { get; set; }

        [Required]
        public Guid ContributorTypeID { get; set; }
        public ContributorType ContributorType { get; set; }
    }
}
