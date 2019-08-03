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
    public class ContributorTypeService : IContributorTypeService
    {

        private readonly ApplicationDbContext _context;


        public ContributorTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContributorTypeViewModel>> Get()
        {
            var ct = await _context.ContributorsType.Include(x => x.Translations).ToListAsync();
            List<ContributorTypeViewModel> contributorTypes = ct.Select(x => x.ToContributorTypeViewModel()).ToList();

            return contributorTypes;
        }

        public async Task<ContributorTypeViewModel> Get(Guid id)
        {
            var ct = await GetContributorType(id);

            if (ct == null)
                return null;

            ContributorTypeViewModel contributorType = ct.ToContributorTypeViewModel();

            return contributorType;
        }

        public async Task<ContributorTypeViewModel> Post(ContributorTypeViewModel model)
        {
            ContributorType contributorType;

            try
            {
                model.Details.ValidateTranslations();

                contributorType = model.ToContributorType();
                _context.ContributorsType.Add(contributorType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return contributorType.ToContributorTypeViewModel();
        }

        public async Task<ContributorTypeViewModel> Put(ContributorTypeViewModel model)
        {
            if (!ContributorTypeExists(model.ID.Value))
                return null;

            try
            {
                model.Details.ValidateTranslations();

                var ct = await GetContributorType(model.ID.Value);

                model.ToContributorType(ct);
                _context.Entry(ct).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return model;
        }

        public async Task<ContributorTypeViewModel> Delete(Guid id)
        {
            if (!ContributorTypeExists(id))
                return null;

            try
            {
                var ct = await GetContributorType(id);

                _context.ContributorsType.Remove(ct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return new ContributorTypeViewModel();
        }

        private bool ContributorTypeExists(Guid id)
        {
            return _context.ContributorsType.Any(e => e.ID == id);
        }
        
        private async Task<ContributorType> GetContributorType(Guid id)
        {
            return await _context.ContributorsType.Include(x => x.Translations).FirstOrDefaultAsync(i => i.ID == id);

        }
    }
}
