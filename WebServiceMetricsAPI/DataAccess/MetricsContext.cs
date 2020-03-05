using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.DataAccess
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class MetricsContext : DbContext
    {
        public DbSet<MetricsResult> MetricsResults { get; set; }
        public DbSet<MetricsRun> MetricsRuns { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=WebServiceMetrics;Trusted_Connection=True;");
        }
    }
}
