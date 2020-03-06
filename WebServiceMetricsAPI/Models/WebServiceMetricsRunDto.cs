using System;

namespace WebServiceMetricsAPI.Models
{

    [Serializable]
    public class WebServiceMetricsRunDto
    {
        public int MetricRunId { get; set; }
        public string RequestUrl { get; set; }
        public string RequestBody { get; set; }
        public int NumberOfRequestsToSend { get; set; }
    }
}
