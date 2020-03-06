using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebServiceMetricsAPI.Entities
{
    public class MetricsRun
    {
        [Key]
        public int MetricRunId { get; set; }
        public string RequestUrl { get; set; }
        public string RequestBody { get; set; }
        public int NumberOfRequestsToSend { get; set; }

        public virtual ICollection<MetricsResult> MetricResults { get; set; }
    }
}
