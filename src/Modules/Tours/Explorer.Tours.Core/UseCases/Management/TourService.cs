using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Management;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Management
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public TourService(ITourRepository tourRepository, IMapper mapper)
        {
            _tourRepository = tourRepository;
            _mapper = mapper;
        }

        public Result<TourDto> Create(CreateTourDto createTour)
        {
            try
            {
                var tour = _tourRepository.Create(new Tour(createTour.AuthorId, createTour.Name, createTour.Description, createTour.Difficult, createTour.Category, createTour.Price, createTour.Status, convertKeyPoints(createTour.KeyPoints)));

                return _mapper.Map<TourDto>(tour);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }

        public List<KeyPoint> convertKeyPoints(List<CreateKeyPointDto> keyPoints)
        {
            return keyPoints.Select(keyPoint => new KeyPoint(
                    keyPoint.Longitude,
                    keyPoint.Latitude,
                    keyPoint.Name,
                    keyPoint.Description,
                    new Image(keyPoint.Image.Filename, Convert.FromBase64String(keyPoint.Image.Data))
                )).ToList();
        }
    }
}
