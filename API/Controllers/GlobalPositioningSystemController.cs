using Microsoft.AspNetCore.Mvc;
using Ford.Tracker.Api.DTO;
using Ford.Tracker.Api.Business;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ford.Tracker.Api.Controllers
{
    [Produces("application/json")]
    [Route("GlobalPositioningSystem")]
    public class GlobalPositioningSystemController : Controller
    {
        private readonly IGlobalPositioningSystemBusiness _globalPositioningSystemBusiness;

        public GlobalPositioningSystemController(IGlobalPositioningSystemBusiness globalPositioningSystemBusiness)
        {
            _globalPositioningSystemBusiness = globalPositioningSystemBusiness;
        }

        [HttpPost]
        [Route("send-current-coordinates")]
        public async Task<IActionResult> SendCurrentCoordinates(GlobalPositioningSystemCreateRequest globalPositioningSystemCreateRequest)
        {
            if (globalPositioningSystemCreateRequest == null)
            {
                return BadRequest();
            }

            await _globalPositioningSystemBusiness.MapAndSendToMessengerAsync(globalPositioningSystemCreateRequest.GlobalPositioningData);

            return Accepted();
        }
                
        [HttpGet]
        [Route("test")]
        public IEnumerable<string> Get()
        {
            //await _bus.Advanced.Routing.Send("Test", new GlobalPositioningSystemMessage { test = "jshedfjks" });
            return new string[] { "value1", "value2" };
        }
    }
}