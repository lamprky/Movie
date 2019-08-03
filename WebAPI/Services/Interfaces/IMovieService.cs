using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Service;

namespace WebAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieViewModel>> Get();

        Task<MovieViewModel> Get(Guid id);

        Task<MovieViewModel> Post(MovieViewModel model);

        Task<MovieViewModel> Put(MovieViewModel model);

        Task<MovieViewModel> Delete(Guid id);
    }
}
