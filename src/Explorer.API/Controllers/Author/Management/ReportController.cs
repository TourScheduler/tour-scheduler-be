using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Management
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/management/reports")]
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("author/{authorId:int}")]
        public ActionResult<ReportDto> GetByAuthorId(int authorId)
        {
            var result = _reportService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpGet("unsolded-tours/author/{authorId:int}")]
        public ActionResult<TourDto> GetUnsoldedToursByAuthorId(int authorId)
        {
            var result = _reportService.GetUnsoldedToursByAuthorId(authorId);
            return CreateResponse(result);
        }
    }
}
