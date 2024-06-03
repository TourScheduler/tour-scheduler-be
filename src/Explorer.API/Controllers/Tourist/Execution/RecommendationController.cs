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

        public RecommendationController(ITourService tourService, IAuthorService authorService)
        {
            _tourService = tourService;
            _authorService = authorService;
        }

        [HttpGet("filter-by-award")]
        public ActionResult<TourDto> FilterByAwardStatus([FromQuery] bool status)
        {
            Debug.WriteLine(status);
            var result = _tourService.GetByAwardStatus(status, _authorService.GetAll().Value
                .Select(a => new AuthorDto(a.Id, a.UserId, a.Points, a.IsAwarded))
                .ToList());
            return CreateResponse(result);
        }
    }
}
