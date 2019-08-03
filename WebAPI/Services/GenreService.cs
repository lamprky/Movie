using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Extensions;
using WebAPI.Models.Database;
using WebAPI.Models.Service;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class GenreService : IGenreService
    {

        private readonly ApplicationDbContext _context;


        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenreViewModel>> Get()
        {
            var g = await _context.Genre.Include(x => x.Translations).ToListAsync();
            List<GenreViewModel> genres = g.Select(x => x.ToGenreViewModel()).ToList();

            return genres;
        }

        public async Task<GenreViewModel> Get(Guid id)
        {
            var g = await GetGenre(id);

            if (g == null)
                return null;

            return g.ToGenreViewModel();
        }

        public async Task<GenreViewModel> Post(GenreViewModel model)
        {
            Genre genre;

            try
            {
                model.Details.ValidateTranslations();

                genre = model.ToGenre();
                _context.Genre.Add(genre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return genre.ToGenreViewModel();
        }

        public async Task<GenreViewModel> Put(GenreViewModel model)
        {
            if (!GenreExists(model.ID.Value))
                return null;

            try
            {
                model.Details.ValidateTranslations();

                var g = await GetGenre(model.ID.Value);

                model.ToGenre(g);
                _context.Entry(g).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return model;
        }

        public async Task<GenreViewModel> Delete(Guid id)
        {
            if (!GenreExists(id))
                return null;

            try
            {
                var g = await GetGenre(id);

                _context.Genre.Remove(g);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return new GenreViewModel();
        }

        private bool GenreExists(Guid id)
        {
            return _context.Genre.Any(e => e.ID == id);
        }

        private async Task<Genre> GetGenre(Guid id)
        {
            return await _context.Genre.Include(x => x.Translations).FirstOrDefaultAsync(i => i.ID == id);
        }
    }
}
