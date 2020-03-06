using System;

namespace WebServiceMetricsAPI.Models
{
    [Serializable]
    public class WebServiceMetricsResultDto
    {
        public string TimeElapsedInMilliseconds { get; set; }
        public string ErrorMessage { get; set; }
    }
}
