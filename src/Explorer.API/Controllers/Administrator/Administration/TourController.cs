using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/tours")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> GetById(int id)
        {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }
    }
}
