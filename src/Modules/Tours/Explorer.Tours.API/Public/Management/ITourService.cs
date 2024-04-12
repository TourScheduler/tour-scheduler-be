using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Management
{
    public interface ITourService
    {
        Result<TourDto> Create(CreateTourDto createTour);
        Result<List<TourDto>> GetByAuthorId(int authorId);
    }
}
