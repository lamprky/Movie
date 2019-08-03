using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Application.Models
{
    [DataContract]
    public class DetailsViewModel
    {
        [DataMember(Name = "id")]
        public Guid? ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "languageId")]
        public Guid LanguageId { get; set; }
    }
}
