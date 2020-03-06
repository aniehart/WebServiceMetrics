using System;
using System.Collections.Generic;

namespace WebServiceMetricsAPI.Models
{
    [Serializable]
    public class WebServiceMetricsResponse
    {
        public int WebServiceMetricsRunId { get; set; }
        public WebServiceMetricsRequest WebServiceMetricsRequestMeasured { get; set; }
        public List<WebServiceMetricsResult> WebServiceMetricsResults { get; set; } = new List<WebServiceMetricsResult>();
    }
}
