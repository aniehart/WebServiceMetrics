using System;
using WebServiceMetricsAPI.BusinessLogic;
using WebServiceMetricsAPI.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceMetricsAPI.Controllers
{
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
                return Content(JsonConvert.SerializeObject(await metricsManager.RunMetrics(request)), "application/json");
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
