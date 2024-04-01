using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TouristDatabaseRepository : ITouristRepository
    {
        private readonly StakeholdersContext _dbContext;

        public TouristDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Tourist Create(Tourist tourist)
        {
            _dbContext.Tourists.Add(tourist);
            _dbContext.SaveChanges();
            return tourist;
        }
    }
}
