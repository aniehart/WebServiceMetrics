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
                var response = new List<WebServiceMetricsResponse>();
                var httpClient = new HttpClient();
                var uri = new Uri(request.RestEndpointToPost);

                for (int i = 1; i <= request.NumberOfRequestsToMake; i++)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var wsResponse = await httpClient.PostAsync(uri, new StringContent(request.RequestBodyToSend, Encoding.Unicode, "application/json"));
                    sw.Stop();

                    response.Add(new WebServiceMetricsResponse
                    {
                        timeElapsedInSeconds = sw.ElapsedMilliseconds.ToString()
                    });
                }

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
