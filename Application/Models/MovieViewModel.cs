using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Application.Models
{
    [DataContract]
    public class MovieViewModel : IGeneralViewModel
    {
        [DataMember(Name = "id")]
        public Guid? ID { get; set; }

        [DataMember(Name = "details")]
        public List<DetailsViewModel> Details { get; set; } = new List<DetailsViewModel>();

        [DataMember(Name = "contributors")]
        public List<Guid> Contributors { get; set; } = new List<Guid>();

        [DataMember(Name = "genres")]
        public List<Guid> Genres { get; set; } = new List<Guid>();
    }
}
