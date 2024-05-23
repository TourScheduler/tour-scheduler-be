using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PurchaseDatabaseRepository : IPurchaseRepository
    {
        private readonly ToursContext _dbContext;

        public PurchaseDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Purchase Create(Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            _dbContext.SaveChanges();
            return purchase;
        }
    }
}
