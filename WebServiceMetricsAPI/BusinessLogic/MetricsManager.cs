using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebServiceMetricsAPI.Models;
using System.Diagnostics;
using System.Text;
using WebServiceMetricsAPI.Entities;
using WebServiceMetricsAPI.Repositories;
using System.Threading;
using System.Linq;

namespace WebServiceMetricsAPI.BusinessLogic
{
    public class MetricsManager
    {
        public async Task<WebServiceMetricsResponse> RunMetrics(WebServiceMetricsRequest request)
        {
            var response = new WebServiceMetricsResponse
            {
                WebServiceMetricsRequestMeasured = request
            };

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

            response.WebServiceMetricsRunId = metricsRunEntity.MetricRunId;

            var tasks = new List<Task>();
            using (var semaphore = new SemaphoreSlim(10))
            {
                for(int i = 0; i < request.NumberOfRequestsToSend; i++) 
                {
                    await semaphore.WaitAsync();
                    tasks.Add(MeasureRequest(semaphore, request, response, metricsRunEntity.MetricRunId));
                }
                await Task.WhenAll(tasks);
            }

            return response;
        }

        public List<WebServiceMetricsRunDto> GetMetricRuns()
        {
            using (var repository = new MetricsRepository())
            {
                return repository.GetMetricsRuns()
                                 .Select(x => new WebServiceMetricsRunDto()
                                 {
                                     MetricRunId = x.MetricRunId,
                                     NumberOfRequestsToSend = x.NumberOfRequestsToSend,
                                     RequestBody = x.RequestBody,
                                     RequestUrl = x.RequestUrl
                                 })
                                 .ToList();
            }
        }
        private static async Task MeasureRequest(SemaphoreSlim semaphore, WebServiceMetricsRequest request, WebServiceMetricsResponse response, int metricRunId)
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
                    await repository.SaveMetricsResult(metricsResultEntity);
                }

                response.WebServiceMetricsResults.Add(new WebServiceMetricsResultDto
                {
                    TimeElapsedInMilliseconds = metricsResultEntity.TimeElapsedInMilliseconds.ToString()
                });
            }
            catch (Exception ex)
            {
               response.WebServiceMetricsResults.Add(new WebServiceMetricsResultDto()
               {
                   ErrorMessage = ex.Message
               });
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
