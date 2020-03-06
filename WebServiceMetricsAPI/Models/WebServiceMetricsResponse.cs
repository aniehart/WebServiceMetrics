using System;

namespace WebServiceMetricsAPI.Models
{
    using System.Collections.Generic;

    [Serializable]
    public class WebServiceMetricsResponse
    {
        public int WebServiceMetricsRunId { get; set; }
        public WebServiceMetricsRequest WebServiceMetricsRequestMeasured { get; set; }
        public List<WebServiceMetricsResult> WebServiceMetricsResults { get; set; }
    }
}
