using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourProblemDatabaseRepository : ITourProblemRepository
    {
        private readonly ToursContext _dbContext;

        public TourProblemDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TourProblem Create(TourProblem tourProblem)
        {
            _dbContext.TourProblems.Add(tourProblem);
            _dbContext.SaveChanges();
            return tourProblem;
        }

        public List<TourProblem> GetAll()
        {
            return _dbContext.TourProblems.ToList();
        }

        public List<TourProblem> GetByTouristId(long touristId)
        {
            return _dbContext.TourProblems.Where(tp => tp.TouristId == touristId).ToList();
        }
    }
}
