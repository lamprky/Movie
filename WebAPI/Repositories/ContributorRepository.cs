using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.Database;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class ContributorRepository : GenericRepository<Contributor>, IContributorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ContributorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
