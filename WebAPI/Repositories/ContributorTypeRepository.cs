using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.Database;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class ContributorTypeRepository : GenericRepository<ContributorType>, IContributorTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ContributorTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
