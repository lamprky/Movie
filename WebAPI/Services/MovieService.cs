using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Extensions;
using WebAPI.Models.Database;
using WebAPI.Models.Service;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovieViewModel>> Get()
        {
            var m = await _context.Movie.Include(x => x.Translations).Include(x => x.MovieContributors).Include(x => x.MovieGenres).ToListAsync();
            List<MovieViewModel> movies = m.Select(x => x.ToMovieViewModel()).ToList();

            return movies;
        }

        public async Task<MovieViewModel> Get(Guid id)
        {
            var m = await GetMovie(id);

            if (m == null)
                return null;

            MovieViewModel contributor = m.ToMovieViewModel();

            return contributor;
        }

        public async Task<MovieViewModel> Post(MovieViewModel model)
        {
            var m = new Movie();
            try
            {
                await ValidateInput(model);

                m = model.ToMovie();
                _context.Movie.Add(m);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return m.ToMovieViewModel();
        }

        public async Task<MovieViewModel> Put(MovieViewModel model)
        {
            if (!MovieExists(model.ID.Value))
                return null;

            try
            {
                await ValidateInput(model);
                var m = await GetMovie(model.ID.Value);

                model.ToMovie(m);
                _context.Entry(m).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return model;
        }

        public async Task<MovieViewModel> Delete(Guid id)
        {
            if (!MovieExists(id))
                return null;

            try
            {
                var m = await GetMovie(id);

                _context.Movie.Remove(m);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return new MovieViewModel();
        }

        private async Task<Movie> GetMovie(Guid id)
        {
            return await _context.Movie
                       .Include(x => x.Translations)
                       .Include(x => x.MovieContributors)
                       .Include(x => x.MovieGenres)
                       .FirstOrDefaultAsync(i => i.ID == id);
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }

        private async Task ValidateInput(MovieViewModel model)
        {
            model.Details.ValidateTranslations();

            var contributors = await _context.Contributor.Where(x => model.Contributors.Contains(x.ID)).ToListAsync();
            model.Contributors.ValidateContributors(contributors);

            var genres = await _context.Genre.Where(x => model.Genres.Contains(x.ID)).ToListAsync();
            model.Genres.ValidateGenres(genres);
        }
    }
}
