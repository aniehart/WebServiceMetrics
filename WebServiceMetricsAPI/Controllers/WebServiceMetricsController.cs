using System;
using WebServiceMetricsAPI.BusinessLogic;
using WebServiceMetricsAPI.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceMetricsAPI.Controllers
{
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class WebServiceMetricsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<WebServiceMetricsResponse>> Post([FromBody] WebServiceMetricsRequest request)
        {
            try
            {
                var metricsManager = new MetricsManager();
                return Content(JsonConvert.SerializeObject(await metricsManager.RunMetrics(request)), "application/json");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<WebServiceMetricsRunDto>> Get()
        {
            try
            {
                var metricsManager = new MetricsManager();
                return Content(JsonConvert.SerializeObject(metricsManager.GetMetricRuns()), "application/json");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }           
        }
    }
}
