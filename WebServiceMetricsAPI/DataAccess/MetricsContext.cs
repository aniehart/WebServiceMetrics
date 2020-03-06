using WebServiceMetricsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebServiceMetricsAPI.DataAccess
{
    public class MetricsContext : DbContext
    {
        public MetricsContext() { }
        public MetricsContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<MetricsResult> MetricsResults { get; set; }
        public DbSet<MetricsRun> MetricsRuns { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("MetricsDatabase");
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=WebServiceMetrics;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
