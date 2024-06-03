using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class AuthorDatabaseRepository : IAuthorRepository
    {
        private readonly StakeholdersContext _dbContext;

        public AuthorDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Author GetByUserId(long userId)
        {
            return _dbContext.Authors.FirstOrDefault(a => a.UserId == userId);
        }

        public Author Update(Author author)
        {
            try
            {
                _dbContext.Authors.Update(author);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return author;
        }
    }
}
