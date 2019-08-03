using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Database;
using WebAPI.Models.Service;

namespace WebAPI.Services.Interfaces
{
    public interface IContributorService
    {
        Task<List<ContributorViewModel>> Get();

        Task<ContributorViewModel> Get(Guid id);

        Task<ContributorViewModel> Post(ContributorViewModel model);

        Task<ContributorViewModel> Put(ContributorViewModel model);

        Task<ContributorViewModel> Delete(Guid id);
    }
}
