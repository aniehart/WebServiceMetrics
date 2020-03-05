using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.Entities
{
    public class MetricResult
    {
        public string Result { get; set; }
        public int TimeElapsedInMilliseconds { get; set; }

        public virtual MetricRun MetricRun { get; set; }
    }
}
