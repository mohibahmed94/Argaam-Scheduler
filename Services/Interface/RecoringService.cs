using Microsoft.EntityFrameworkCore;
using ArgaamSchedular.Entities;
using ArgaamSchedular.Services.Implementations;

namespace ArgaamSchedular.Services.Interface
{
    public class RecoringService : IRecoringService
    {
        public readonly ArgaamPlusContext _context;
        public RecoringService(ArgaamPlusContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }
        public void CreateJob(ScheduledJobModel model)
        {
            ScheduledJob job = new ScheduledJob()
            {
                TypeId = model.TypeId,
                Url = model.Url,
                Frequency = model.Frequency,
                ScheduleTime = model.ScheduleTime
            };

            _context.ScheduledJobs.Add(job);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ScheduledJob>> GetAllJobs()
        {
            
             var jobs =  _context.ScheduledJobs.ToList();
            return jobs;
            
        }
    }
}
