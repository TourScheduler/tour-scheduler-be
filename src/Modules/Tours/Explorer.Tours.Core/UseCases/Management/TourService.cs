using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
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

        public Result<List<TourDto>> GetByAuthorId(int authorId)
        {
            List<Tour> authorTours = _tourRepository.GetByAuthorId(authorId);
            return _mapper.Map<List<TourDto>>(authorTours);
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

        public Result<TourDto> Publish(int id)
        {
            try
            {
                var tour = _tourRepository.GetById(id);
                if (tour != null)
                {
                    tour.Publish();
                    tour = _tourRepository.Update(tour);
                }

                return _mapper.Map<TourDto>(tour);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> GetById(int id)
        {
            try
            {
                Tour tour = _tourRepository.GetById(id);
                return _mapper.Map<TourDto>(tour);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> Archive(int id)
        {
            try
            {
                var tour = _tourRepository.GetById(id);
                if (tour != null)
                {
                    tour.Archive();
                    tour = _tourRepository.Update(tour);

                    return _mapper.Map<TourDto>(tour);
                }

                return Result.Fail(FailureCode.NotFound).WithError($"Tour with ID {id} not found.");
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<TourDto>> GetAuthorToursByStatus(int authorId, int status)
        {
            List<Tour> filteredTours = _tourRepository.GetAuthorToursByStatus(authorId, status);
            return _mapper.Map<List<TourDto>>(filteredTours);
        }
    }
}
