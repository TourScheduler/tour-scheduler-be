using Explorer.BuildingBlocks.Core.UseCases;
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

        [HttpGet("author/{authorId:int}")]
        public ActionResult<TourDto> GetByAuthorId(int authorId)
        {
            var result = _tourService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/publish")]
        public ActionResult<TourDto> Publish(int id)
        {
            var result = _tourService.Publish(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> GetById(int id)
        {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/archive")]
        public ActionResult<TourDto> Archive(int id)
        {
            var result = _tourService.Archive(id);
            return CreateResponse(result);
        }

        [HttpGet("author/{authorId:int}/filter")]
        public ActionResult<TourDto> FilterByStatus(int authorId, [FromQuery] int status)
        {
            throw new NotImplementedException();
        }
    }
}
