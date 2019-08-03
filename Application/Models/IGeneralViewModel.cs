using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public interface IGeneralViewModel
    {
        Guid? ID { get; set; }

        List<DetailsViewModel> Details { get; set; }
    }
}
