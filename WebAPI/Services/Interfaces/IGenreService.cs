using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Service;

namespace WebAPI.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreViewModel>> Get();

        Task<GenreViewModel> Get(Guid id);

        Task<GenreViewModel> Post(GenreViewModel model);

        Task<GenreViewModel> Put(GenreViewModel model);

        Task<GenreViewModel> Delete(Guid id);

    }
}
