using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist")]
    public class TouristController : BaseApiController
    {
        private readonly ITouristService _touristService;

        public TouristController(ITouristService touristService)
        {
            _touristService = touristService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Stakeholders.API.Dtos.TouristDto> GetById(int id)
        {
            var result = _touristService.GetById(id);
            return CreateResponse(result);
        }
    }
}
