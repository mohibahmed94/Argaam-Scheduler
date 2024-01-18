using ArgaamSchedular.Entities;

namespace ArgaamSchedular.Services.Implementations
{
    public interface IRecoringService
    {
        public void CreateJob(ScheduledJobModel model);
        public Task<IEnumerable<ScheduledJob>> GetAllJobs();

    }
}
