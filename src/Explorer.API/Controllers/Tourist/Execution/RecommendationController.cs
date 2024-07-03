using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/recommendation")]
    public class RecommendationController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly IAuthorService _authorService;
        private readonly ITouristService _touristService;

        public RecommendationController(ITourService tourService, IAuthorService authorService, ITouristService touristService)
        {
            _tourService = tourService;
            _authorService = authorService;
            _touristService = touristService;
        }

        [HttpGet("filter-by-award")]
        public ActionResult<TourDto> FilterByAwardStatus([FromQuery] bool status)
        {
            var result = _tourService.GetByAwardStatus(status, _authorService.GetAll().Value
                .Select(a => new AuthorDto(a.Id, a.UserId, a.Points, a.IsAwarded))
                .ToList());
            return CreateResponse(result);
        }

        [HttpGet("tours/tourist/{touristId:int}")]
        public ActionResult<TourDto> GetByTouristInterests(int touristId)
        {
            var tourist = _touristService.GetById(touristId).Value;
            var result = _tourService.GetByTouristInterests(new TouristDto(tourist.Id, tourist.Username, tourist.Password, Tours.API.Dtos.UserRole.Tourist, tourist.IsActive, tourist.Name, tourist.Surname, tourist.Email, tourist.Interests.Select(i => new InterestDto((Tours.API.Dtos.InterestType)i.Type)).ToList()));
            return CreateResponse(result);
        }

        [HttpGet("tours/tourist/{touristId:int}/difficult")]
        public ActionResult<TourDto> GetByTouristInterestsAndDifficult([FromQuery] int difficult, int touristId)
        {
            var tourist = _touristService.GetById(touristId).Value;
            var result = _tourService.GetByTouristInterestsAndDifficult(difficult, new TouristDto(tourist.Id, tourist.Username, tourist.Password, Tours.API.Dtos.UserRole.Tourist, tourist.IsActive, tourist.Name, tourist.Surname, tourist.Email, tourist.Interests.Select(i => new InterestDto((Tours.API.Dtos.InterestType)i.Type)).ToList()));
            return CreateResponse(result);
        }
    }
}
