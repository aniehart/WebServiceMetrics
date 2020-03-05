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
    using System.Threading;
    using System.Net;

    public class MetricsManager
    {
        public async Task<List<WebServiceMetricsResponse>> RunMetrics(WebServiceMetricsRequest request)
        {
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

            var tasks = new List<Task>();
            using (var semaphore = new SemaphoreSlim(10))
            {
                for(int i = 0; i < request.NumberOfRequestsToSend; i++) 
                {
                    await semaphore.WaitAsync();
                    tasks.Add(SendRequest(semaphore, request, response, metricsRunEntity.MetricRunId));
                }
                await Task.WhenAll(tasks);
            }

            return response;
        }
        private static async Task SendRequest(SemaphoreSlim semaphore, WebServiceMetricsRequest request, List<WebServiceMetricsResponse> response, int metricRunId)
        {
            try
            {
                var httpClient = new HttpClient();
                var uri = new Uri(request.RequestUrl);
                var sw = new Stopwatch();

                sw.Start();
                var wsResponse = await httpClient.PostAsync(uri, new StringContent(request.RequestBody, Encoding.Unicode, "application/json"));
                sw.Stop();

                var metricsResultEntity = new MetricsResult()
                {
                    MetricsRunId = metricRunId,
                    TimeElapsedInMilliseconds = (int)sw.ElapsedMilliseconds,
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
            }
            catch (Exception ex)
            {
               response.Add(new WebServiceMetricsResponse()
               {
                   errorMessage = ex.Message
               });
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
