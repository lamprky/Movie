using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Models.Service
{
    [DataContract]
    public class ContributorViewModel
    {
        [DataMember(Name = "id")]
        public Guid? ID { get; set; }

        [DataMember(Name = "details")]
        public List<DetailsViewModel> Details { get; set; } = new List<DetailsViewModel>();

        [DataMember(Name = "contributorTypes")]
        public List<Guid> ContributorTypes { get; set; } = new List<Guid>();

    }
}
