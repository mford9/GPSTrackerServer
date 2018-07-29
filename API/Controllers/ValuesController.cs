using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using Ford.Tracker.Api.Messaging.Payload;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ford.Tracker.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        public ValuesController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {            
            HostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //await _bus.Advanced.Routing.Send("Test", new GlobalPositioningSystemMessage { test = "jshedfjks" });
            return new string[] { HostingEnvironment.IsProduction().ToString(), Configuration.GetValue<string>("ServiceBusConnectionString")  };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
