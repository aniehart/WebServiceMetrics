using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.Models
{
    [Serializable]
    public class WebServiceMetricsResult
    {
        public string TimeElapsedInMilliseconds { get; set; }
        public string ErrorMessage { get; set; }
    }
}
