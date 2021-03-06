﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.Entities
{
    public class MetricsResult
    {
        [Key]
        public int MetricsResultId { get; set; }
        public int MetricsRunId { get; set; }
        public string Result { get; set; }
        public int TimeElapsedInMilliseconds { get; set; }

        public virtual MetricsRun MetricRun { get; set; }
    }
}
