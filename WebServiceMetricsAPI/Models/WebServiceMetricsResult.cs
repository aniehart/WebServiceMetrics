using System;

namespace WebServiceMetricsAPI.Models
{
    [Serializable]
    public class WebServiceMetricsResult
    {
        public string TimeElapsedInMilliseconds { get; set; }
        public string ErrorMessage { get; set; }
    }
}
