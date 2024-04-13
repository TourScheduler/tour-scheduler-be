using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        Tour Create(Tour tour);
        List<Tour> GetByAuthorId(int authorId);
        Tour GetById(int id);
        Tour Update(Tour tour);
    }
}
