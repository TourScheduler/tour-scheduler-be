using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/tour-problem")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;

        public TourProblemController(ITourProblemService tourProblemService)
        {
            _tourProblemService = tourProblemService;
        }

        [HttpPost]
        public ActionResult<TourProblemDto> Create([FromBody] CreateTourProblemDto createTourProblem)
        {
            var result = _tourProblemService.Create(createTourProblem);
            return CreateResponse(result);
        }

        [HttpGet("tourist/{touristId:int}")]
        public ActionResult<TourProblemDto> GetByTouristId(int touristId)
        {
            var result = _tourProblemService.GetByTouristId(touristId);
            return CreateResponse(result);
        }
    }
}
