using System.Threading.Tasks;
using WebServiceMetricsAPI.DataAccess;
using WebServiceMetricsAPI.Entities;

namespace WebServiceMetricsAPI.Repositories
{
    public class MetricsRepository : RepositoryBase<MetricsContext>
    {
        public async Task<MetricsRun> SaveMetricsRun(MetricsRun metricsRunEntity)
        {
            var existingEntity = await this.Context.MetricsRuns.FindAsync(metricsRunEntity.MetricRunId);
            if (existingEntity == null)
            {
                this.Context.MetricsRuns.Add(metricsRunEntity);
            }
            else
            {
                this.Context.MetricsRuns.Update(metricsRunEntity);
            }

            await this.Context.SaveChangesAsync();

            return await this.Context.MetricsRuns.FindAsync(metricsRunEntity.MetricRunId);
        }

        public async Task<MetricsResult> SaveMetricsResult(MetricsResult metricsResultEntity)
        {
            var existingEntity = await this.Context.MetricsResults.FindAsync(metricsResultEntity.MetricsResultId);
            if (existingEntity == null)
            {
                this.Context.MetricsResults.Add(metricsResultEntity);
            }
            else
            {
                this.Context.MetricsResults.Update(metricsResultEntity);
            }

            await this.Context.SaveChangesAsync();

            return await this.Context.MetricsResults.FindAsync(metricsResultEntity.MetricsResultId);
        }
    }
}
