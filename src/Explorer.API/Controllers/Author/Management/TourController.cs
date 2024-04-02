using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Management;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Tours
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/management/tours")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] CreateTourDto createTour)
        {
            var result = _tourService.Create(createTour);
            return CreateResponse(result);
        }
    }
}
