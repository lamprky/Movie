using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Application.Models
{
    [DataContract]
    public class ContributorViewModel : IGeneralViewModel
    {
        [DataMember(Name = "id")]
        public Guid? ID { get; set; }

        [DataMember(Name = "details")]
        public List<DetailsViewModel> Details { get; set; } = new List<DetailsViewModel>();

        [DataMember(Name = "contributorTypes")]
        public List<Guid> ContributorTypes { get; set; } = new List<Guid>();

    }
}
