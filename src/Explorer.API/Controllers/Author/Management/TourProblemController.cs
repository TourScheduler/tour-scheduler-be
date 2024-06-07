using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Management
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/management/tour-problem")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;

        public TourProblemController(ITourProblemService tourProblemService)
        {
            _tourProblemService = tourProblemService;
        }

        [HttpGet("author/{authorId:int}")]
        public ActionResult<TourProblemDto> GetByAuthorId(int authorId)
        {
            var result = _tourProblemService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/status")]
        public ActionResult<TourProblemDto> UpdateStatus(int id, [FromBody] ProblemStatus status)
        {
            var result = _tourProblemService.UpdateStatus(id, status);
            return CreateResponse(result);
        }
    }
}
