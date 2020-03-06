using System;

namespace WebServiceMetricsAPI.Models
{
    [Serializable]
    public class WebServiceMetricsRequest
    {
        public int NumberOfRequestsToSend { get; set; }
        public string RequestBody { get; set; }
        public string RequestUrl { get; set; }
    }
}
