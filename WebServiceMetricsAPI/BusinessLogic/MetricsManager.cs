using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.BusinessLogic
{
    using Models;
    using System.Diagnostics;
    using System.Text;
    using Entities;
    using Repositories;

    public class MetricsManager
    {
        public async Task<List<WebServiceMetricsResponse>> RunMetrics(WebServiceMetricsRequest request)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(request.RequestUrl);
            var response = new List<WebServiceMetricsResponse>();

            var metricsRunEntity = new MetricsRun()
            {
                RequestBody = request.RequestBody,
                RequestUrl = request.RequestUrl,
                NumberOfRequestsToSend = request.NumberOfRequestsToSend
            };

            using (var repository = new MetricsRepository())
            {
                metricsRunEntity = await repository.SaveMetricsRun(metricsRunEntity);
            }

            for (int i = 1; i <= request.NumberOfRequestsToSend; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                var wsResponse = await httpClient.PostAsync(uri, new StringContent(request.RequestBody, Encoding.Unicode, "application/json"));
                sw.Stop();

                var metricsResultEntity = new MetricsResult()
                {
                    MetricsRunId = metricsRunEntity.MetricRunId,
                    TimeElapsedInMilliseconds = (int) sw.ElapsedMilliseconds,
                    Result = wsResponse.StatusCode.ToString()
                };

                //use separate repository each time since .NET/EF Core cannot multi-thread the same DbContext...
                using (var repository = new MetricsRepository())
                {
                    metricsResultEntity = await repository.SaveMetricsResult(metricsResultEntity);
                }

                response.Add(new WebServiceMetricsResponse
                {
                    timeElapsedInSeconds = sw.ElapsedMilliseconds.ToString()
                });

                //create MetricsResults
            }

            return response;
        }
    }
}
