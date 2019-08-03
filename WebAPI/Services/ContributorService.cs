using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Extensions;
using WebAPI.Models.Database;
using WebAPI.Models.Service;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class ContributorService : IContributorService
    {
        private readonly ApplicationDbContext _context;

        public ContributorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContributorViewModel>> Get()
        {
            var c = await _context.Contributor.Include(x => x.Translations).Include(x => x.ContributorContributorTypes).ToListAsync();
            List<ContributorViewModel> contributors = c.Select(x => x.ToContributorViewModel()).ToList();

            return contributors;
        }

        public async Task<ContributorViewModel> Get(Guid id)
        {
            var c = await GetContributor(id);

            if (c == null)
                return null;

            ContributorViewModel contributor = c.ToContributorViewModel();

            return contributor;
        }

        public async Task<ContributorViewModel> Post(ContributorViewModel model)
        {
            var contributor = new Contributor();
            try
            {
                await ValidateInput(model);

                contributor = model.ToContributor();
                _context.Contributor.Add(contributor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return contributor.ToContributorViewModel();
        }

        public async Task<ContributorViewModel> Put(ContributorViewModel model)
        {
            if (!ContributorExists(model.ID.Value))
                return null;

            try
            {
                await ValidateInput(model);
                var c = await GetContributor(model.ID.Value);

                model.ToContributor(c);
                _context.Entry(c).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return model;
        }

        public async Task<ContributorViewModel> Delete(Guid id)
        {
            if (!ContributorExists(id))
                return null;

            try
            {
                var c = await GetContributor(id);

                _context.Contributor.Remove(c);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return new ContributorViewModel();
        }

        private async Task<Contributor> GetContributor(Guid id)
        {
            return await _context.Contributor
                       .Include(x => x.Translations)
                       .Include(x => x.ContributorContributorTypes)
                       .FirstOrDefaultAsync(i => i.ID == id);

        }

        private bool ContributorExists(Guid id)
        {
            return _context.Contributor.Any(e => e.ID == id);
        }

        private async Task ValidateInput(ContributorViewModel model)
        {
            model.Details.ValidateTranslations();

            var contributorTypes = await _context.ContributorsType.Where(x => model.ContributorTypes.Contains(x.ID)).ToListAsync();
            model.ContributorTypes.ValidateContributorTypes(contributorTypes);
        }
    }
}
