using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
