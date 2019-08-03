using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database
{
    [Table("Language")]
    public class Language
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(2)]
        [Required]
        public string Code { get; set; }

    }
}
