using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceMetricsAPI.Controllers
{
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using BusinessLogic;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class WebServiceMetricsController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WebServiceMetricsRequest request)
        {
            try
            {
                var response = new WebServiceMetricsResponse();
                var metricsManager = new MetricsManager();
                response = await metricsManager.RunMetrics(request);
                return Content(JsonConvert.SerializeObject(response), "application/json");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
