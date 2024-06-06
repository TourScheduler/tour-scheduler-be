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
        
        public TourProblemService(ITourProblemRepository tourProblemRepository)
        {
            _tourProblemRepository = tourProblemRepository;
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
    }
}
