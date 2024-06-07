using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class TourProblemService : ITourProblemService
    {
        private readonly ITourProblemRepository _tourProblemRepository;
        private readonly ITourRepository _tourRepository;
        
        public TourProblemService(ITourProblemRepository tourProblemRepository, ITourRepository tourRepository)
        {
            _tourProblemRepository = tourProblemRepository;
            _tourRepository = tourRepository;
        }

        public Result<TourProblemDto> Create(CreateTourProblemDto createTourProblem)
        {
            try
            {
                var tourProblem = _tourProblemRepository.Create(new TourProblem(createTourProblem.TouristId, createTourProblem.TourId, createTourProblem.Name, createTourProblem.Description, Domain.ProblemStatus.Pending));

                return new TourProblemDto(tourProblem.Id, tourProblem.TouristId, tourProblem.TourId, tourProblem.Name, tourProblem.Description, (API.Dtos.ProblemStatus)tourProblem.Status);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }

        public Result<List<TourProblemDto>> GetByAuthorId(long authorId)
        {
            List<TourProblemDto> tourProblems = _tourProblemRepository
                .GetAll()
                .Where(tp => _tourRepository.GetById((int)tp.TourId).AuthorId == authorId)
                .Select(tp => new TourProblemDto(tp.Id, tp.TouristId, tp.TourId, tp.Name, tp.Description, (API.Dtos.ProblemStatus)tp.Status))
                .ToList();

            return tourProblems;
        }

        public Result<List<TourProblemDto>> GetByStatus(int status)
        {
            List<TourProblemDto> tourProblems = _tourProblemRepository
                .GetByStatus(status)
                .Select(tp => new TourProblemDto(tp.Id, tp.TouristId, tp.TourId, tp.Name, tp.Description, (API.Dtos.ProblemStatus)tp.Status))
                .ToList();

            return tourProblems;
        }

        public Result<List<TourProblemDto>> GetByTouristId(long touristId)
        {
            List<TourProblemDto> tourProblems = _tourProblemRepository
                .GetByTouristId(touristId)
                .Select(tp => new TourProblemDto(tp.Id, tp.TouristId, tp.TourId, tp.Name, tp.Description, (API.Dtos.ProblemStatus)tp.Status))
                .ToList();

            return tourProblems;
        }

        public Result<TourProblemDto> UpdateStatus(int id, API.Dtos.ProblemStatus status)
        {
            try
            {
                var tourProblem = _tourProblemRepository.GetById(id);

                tourProblem.ChangeStatus((Domain.ProblemStatus)status);
                tourProblem = _tourProblemRepository.Update(tourProblem);

                return new TourProblemDto(tourProblem.Id, tourProblem.TouristId, tourProblem.TourId, tourProblem.Name, tourProblem.Description, (API.Dtos.ProblemStatus)tourProblem.Status);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }
    }
}
