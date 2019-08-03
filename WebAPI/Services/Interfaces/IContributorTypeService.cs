using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Service;

namespace WebAPI.Services.Interfaces
{
    public interface IContributorTypeService
    {
        Task<List<ContributorTypeViewModel>> Get();

        Task<ContributorTypeViewModel> Get(Guid id);

        Task<ContributorTypeViewModel> Post(ContributorTypeViewModel model);

        Task<ContributorTypeViewModel> Put(ContributorTypeViewModel model);

        Task<ContributorTypeViewModel> Delete(Guid id);

    }
}
