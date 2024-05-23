using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Management;
using Explorer.Tours.Core.UseCases.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/purchase")]
    public class PurchaseController : BaseApiController
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ITourService _tourService;

        public PurchaseController(IPurchaseService purchaseService, ITourService tourService)
        {
            _purchaseService = purchaseService;
            _tourService = tourService;
        }

        [HttpPost]
        public ActionResult<List<PurchaseDto>> Create([FromQuery] string recipientEmail, [FromBody] List<CreatePurchaseDto>  createPurchasesDto)
        {
            var result = _purchaseService.Create(recipientEmail, createPurchasesDto);
            return CreateResponse(result);
        }

        [HttpGet("tours/published")]
        public ActionResult<TourDto> GetPublishedTours()
        {
            var result = _tourService.GetPublishedTours();
            return CreateResponse(result);
        }

        [HttpGet("tours/{id:int}")]
        public ActionResult<TourDto> GetById(int id)
        {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }
    }
}
