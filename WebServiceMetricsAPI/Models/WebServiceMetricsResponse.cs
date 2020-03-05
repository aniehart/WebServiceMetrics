using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.Models
{
    [Serializable]
    public class WebServiceMetricsResponse
    {
        public string timeElapsedInSeconds { get; set; }
        public string errorMessage { get; set; }
    }
}
