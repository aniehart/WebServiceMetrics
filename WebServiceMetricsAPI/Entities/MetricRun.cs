using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.Entities
{
    public class MetricRun
    {
        public int MetricRunId { get; set; }
        public string RequestUrl { get; set; }
        public string RequestBody { get; set; }
        public int NumberOfRequestsToSend { get; set; }

        public virtual ICollection<MetricResult> MetricResults { get; set; }
    }
}
