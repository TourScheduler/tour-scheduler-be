using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class ReportService : IReportService, IInternalReportService
    {
        private readonly IServiceScope _scope;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _scope = serviceScopeFactory.CreateScope();
            _purchaseRepository = _scope.ServiceProvider.GetRequiredService<IPurchaseRepository>();
            _tourRepository = _scope.ServiceProvider.GetRequiredService<ITourRepository>();
            _reportRepository = _scope.ServiceProvider.GetRequiredService<IReportRepository>();
            _mapper = mapper;
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
            foreach (var p in _purchaseRepository.FindPurchasesByMonthAndYear(DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).Year))
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

        public Result<List<ReportDto>> GetByAuthorId(int authorId)
        {
            List<ReportDto> reports = _reportRepository.GetByAuthorId(authorId)
                .Select(ar => new ReportDto(
                    ar.Id,
                    ar.AuthorId,
                    ar.CreatedAt,
                    ar.CreatedFor,
                    ar.Count,
                    ar.Sum,
                    ar.Percentage,
                    ar.BestSellingTours.Select(t => _mapper.Map<TourDto>(_tourRepository.GetById(t))).ToList(),
                    ar.UnsoldedTours.Select(t => _mapper.Map<TourDto>(_tourRepository.GetById(t))).ToList()
                ))
                .ToList();

            return reports;
        }

        public Result<List<TourDto>> GetUnsoldedToursByAuthorId(int authorId)
        {
            var authorTours = _tourRepository.GetByAuthorId(authorId);
            var reports = _reportRepository.GetByAuthorId(authorId).OrderByDescending(r => r.CreatedFor).ToList();

            var unsoldedTours = authorTours.Where(at =>
                Enumerable.Range(0, Math.Max(reports.Count - 3, 0))
                    .Any(i => i + 3 < reports.Count &&
                              reports[i].UnsoldedTours.Contains((int)at.Id) &&
                              reports[i + 1].UnsoldedTours.Contains((int)at.Id) &&
                              reports[i + 2].UnsoldedTours.Contains((int)at.Id))
            ).Select(at => _mapper.Map<TourDto>(at)).ToList();

            return unsoldedTours;
        }

        public long FindAuthorByMostSoldedTours()
        {
            return _reportRepository
                .GetByDate(DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).Year)
                .OrderByDescending(report => report.Count)
                .Select(report => report.AuthorId)
                .FirstOrDefault();
        }
    }
}
