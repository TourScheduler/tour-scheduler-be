using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/execution/purchase")]
    public class PurchaseController : BaseApiController
    {
        [HttpPost]
        public ActionResult<CreatePurchaseDto> Create([FromBody] CreatePurchaseDto createPurchaseDto)
        {
            throw new NotImplementedException();
        }
    }
}
