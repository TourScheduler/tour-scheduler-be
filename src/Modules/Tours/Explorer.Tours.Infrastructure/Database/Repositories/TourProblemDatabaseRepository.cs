using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
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

        public TourProblem GetById(long id)
        {
            return _dbContext.TourProblems.FirstOrDefault(t => t.Id == id);
        }

        public List<TourProblem> GetByStatus(int status)
        {
            return _dbContext.TourProblems.Where(tp => (int)tp.Status == status).ToList();
        }

        public List<TourProblem> GetByTouristId(long touristId)
        {
            return _dbContext.TourProblems.Where(tp => tp.TouristId == touristId).ToList();
        }

        public List<TourProblem> GetByTouristIdAndStatus(long touristId, int status)
        {
            return _dbContext.TourProblems.Where(tp => tp.TouristId == touristId && (int)tp.Status == status).ToList();
        }

        public TourProblem Update(TourProblem tourProblem)
        {
            try
            {
                _dbContext.TourProblems.Update(tourProblem);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return tourProblem;
        }
    }
}
