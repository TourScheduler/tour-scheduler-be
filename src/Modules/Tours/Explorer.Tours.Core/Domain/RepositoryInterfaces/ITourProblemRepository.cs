using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourProblemRepository
    {
        TourProblem Create(TourProblem tourProblem);
        List<TourProblem> GetByTouristId(long touristId);
        List<TourProblem> GetAll();
        TourProblem Update(TourProblem tourProblem);
        TourProblem GetById(long id);
        List<TourProblem> GetByStatus(int status);
        List<TourProblem> GetByTouristIdAndStatus(long touristId, int status);
    }
}
