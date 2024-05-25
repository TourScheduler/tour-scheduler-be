using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class ReportService : IReportService
    {
        private readonly IServiceScope _scope;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IReportRepository _reportRepository;

        public ReportService(IServiceScopeFactory serviceScopeFactory)
        {
            _scope = serviceScopeFactory.CreateScope();
            _purchaseRepository = _scope.ServiceProvider.GetRequiredService<IPurchaseRepository>();
            _tourRepository = _scope.ServiceProvider.GetRequiredService<ITourRepository>();
            _reportRepository = _scope.ServiceProvider.GetRequiredService<IReportRepository>();
        }

        public void Create()
        {
            List<long> authorIds = FindPurchasesAuthors();

            List<Purchase> authorPurchases;
            foreach (var a in authorIds)
            {
                authorPurchases = FindPurchasesByMonthAndYear(a);

                var totalTours = authorPurchases.Count;
                var totalCount = authorPurchases.Sum(p => _tourRepository.GetById((int)p.TourId).Price);

                decimal percentageChange = FindPercentageChange(a, totalCount);

                List<int> bestSellingTours = FindBestSellingTours(authorPurchases, a);
                List<int> unsoldedTours = FindUnsoldedTours(authorPurchases, a);

                var report = _reportRepository.Create(new Report(a, DateTime.Now.ToUniversalTime(), DateTime.Now.AddMonths(-1).ToUniversalTime(), totalTours, totalCount, percentageChange, bestSellingTours, unsoldedTours));
            }
        }

        public List<int> FindUnsoldedTours(List<Purchase> authorPurchases, long a)
        {
            List<int> unsoldedTours = new List<int>();
            foreach (var tour in _tourRepository.GetByAuthorId((int)a))
            {
                bool tourSold = false;
                foreach (var purchase in authorPurchases)
                {
                    if (tour.Id == purchase.TourId)
                    {
                        tourSold = true;
                        break;
                    }
                }

                if (!tourSold)
                {
                    unsoldedTours.Add((int)tour.Id);
                }
            }

            return unsoldedTours;
        }

        public List<int> FindBestSellingTours(List<Purchase> authorPurchases, long a)
        {
            int bestSellingTourId;
            int bestSellingTourCount = 0;
            List<int> bestSellingTours = new List<int>();
            foreach (var t in _tourRepository.GetByAuthorId((int)a))
            {
                int tourCount = 0;
                foreach (var p in authorPurchases)
                {
                    if (t.Id == p.TourId)
                    {
                        tourCount++;
                    }
                }

                if (tourCount > bestSellingTourCount)
                {
                    bestSellingTourId = (int)t.Id;
                    bestSellingTours.Add(bestSellingTourId);
                }
            }

            return bestSellingTours;
        }

        public decimal FindPercentageChange(long a, double totalCount)
        {
            var previousReport = _reportRepository.FindReportByAuthorAndDate(DateTime.Now.AddMonths(-2).Month, DateTime.Now.AddMonths(-2).Year, a);
            decimal percentageChange = 0;
            if (previousReport != null)
            {
                percentageChange = (decimal)(((totalCount - previousReport.Sum) / previousReport.Sum) * 100);
            }

            return percentageChange;
        }

        public List<Purchase> FindPurchasesByMonthAndYear(long a)
        {
            List<Purchase> authorPurchases = new List<Purchase>();
            foreach (var p in _purchaseRepository.FindPurchasesByMonthAndYear(DateTime.Now.Month, DateTime.Now.Year)) // izmeniti kasnije
            {
                Tour tour = _tourRepository.GetById((int)p.TourId);
                if (tour.AuthorId == a)
                {
                    authorPurchases.Add(p);
                }
            }

            return authorPurchases;
        }

        public List<long> FindPurchasesAuthors()
        {
            List<long> authorIds = new List<long>();
            foreach (var p in _purchaseRepository.GetAll())
            {
                Tour tour = _tourRepository.GetById((int)p.TourId);
                if (!authorIds.Contains(tour.AuthorId))
                {
                    authorIds.Add(tour.AuthorId);
                }
            }

            return authorIds;
        }
    }
}
