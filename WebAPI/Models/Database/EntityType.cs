using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("EntityType")]

    public class EntityType
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
