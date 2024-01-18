using Hangfire.Server;
using ArgaamSchedular.Entities;

namespace ArgaamSchedular.Services
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        //void ReccuringJob(PerformContext context,List<ExternalJob> jobs);
        void DelayedJob();
        void ContinuationJob();
        //void ReccuringJob(IEnumerable<ExternalJob> jobs);
        void ReccuringJob(Task<IEnumerable<ScheduledJob>> jobs);
    }
}
