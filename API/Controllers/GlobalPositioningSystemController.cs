using Microsoft.AspNetCore.Mvc;
using Ford.Tracker.Api.DTO;
using Ford.Tracker.Api.Business;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ford.Tracker.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class GlobalPositioningSystemController : Controller
    {
        private readonly IGlobalPositioningSystemBusiness _globalPositioningSystemBusiness;

        public GlobalPositioningSystemController(IGlobalPositioningSystemBusiness globalPositioningSystemBusiness)
        {
            _globalPositioningSystemBusiness = globalPositioningSystemBusiness;
        }

        [HttpPost]
        [Route("send-current-coordinates")]
        public async Task<IActionResult> SendCurrentCoordinates([FromBody]GlobalPositioningSystemCreateRequest globalPositioningSystemCreateRequest)
        {
            if (globalPositioningSystemCreateRequest == null)
            {
                return BadRequest();
            }

            await _globalPositioningSystemBusiness.MapAndSendToMessengerAsync(globalPositioningSystemCreateRequest.GlobalPositioningData);

            return Accepted();
        }        
    }
}