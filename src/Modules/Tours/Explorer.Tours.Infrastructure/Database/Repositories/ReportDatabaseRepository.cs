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
    public class ReportDatabaseRepository : IReportRepository
    {
        private readonly ToursContext _dbContext;

        public ReportDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Report Create(Report report)
        { 
            _dbContext.Reports.Add(report);
            _dbContext.SaveChanges();
            return report;
        }

        public Report FindReportByAuthorAndDate(int month, int year, long authorId)
        {
            return _dbContext.Reports.FirstOrDefault(r => r.AuthorId == authorId && r.CreatedFor.Month == month && r.CreatedFor.Year == year);
        }
    }
}
