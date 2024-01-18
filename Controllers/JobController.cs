using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArgaamSchedular.Enums;
using ArgaamSchedular.Services;
using ArgaamSchedular.Services.Implementations;
using ArgaamSchedular.Services.Interface;
using System.Web;

namespace ArgaamSchedular.Controllers
{
  
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IJobTestService _jobTestService;
        private readonly IRecoringService _recoringService;

        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public JobController(IJobTestService jobTestService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IRecoringService recoringService)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _recoringService = recoringService;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _jobTestService.DelayedJob(), TimeSpan.FromSeconds(60));
            return Ok();
        }

        [HttpGet("/Scheduled/create/newJob")]
        public ActionResult CreateReccuringJob(string Url, string frequency,string scheduleFor)
        {

            //_recurringJobManager.AddOrUpdate("JobId", () => _jobTestService.ReccuringJob(null), Cron.Minutely);
            //_context.ghjk.add(new oobb() { 
            //})



            ScheduledJobModel model = new ScheduledJobModel()
            {
                TypeId = (int)JobType.External,
                Url = HttpUtility.UrlDecode(Url),
                Frequency = frequency,
                ScheduleTime = scheduleFor
            };

            _recoringService.CreateJob(model);
            return Ok();
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult ReccuringJob()
        {

            var jobs = _recoringService.GetAllJobs();
            
            _jobTestService.ReccuringJob(jobs);


            //_recurringJobManager.AddOrUpdate("JobId", () => _jobTestService.ReccuringJob(jobs), Cron.Minutely);
            _jobTestService.ReccuringJob(jobs);

            //ExeternalJobModel model = new ExeternalJobModel() { Delay = delay, Url = HttpUtility.UrlDecode(Url), Params = "test", JobId = 1, TypeId = (int)JobType.External };
            //_recoringService.CreateJob(model);
            return Ok();
        }



        [HttpPost("Schedule")]
        public IActionResult Schedule(string url, DateTime executionDateTime, string methodType, string requestBody = "")
        {
            if (methodType == "GET")
            {
                RecurringJob.AddOrUpdate($"dynamic-job-{Guid.NewGuid()}", () => Schedular.SendHttpRequest(url), Cron.Minutely());
                return Ok($"Job scheduled to run daily at {executionDateTime} for URL: {url}");
            }
            else if(methodType == "POST")
            {
                RecurringJob.AddOrUpdate($"dynamic-job-{Guid.NewGuid()}", () => Schedular.SendHttpPostRequest(url, requestBody), Cron.Minutely());
                return Ok($"Job scheduled to run daily at {executionDateTime} for URL: {url}");
            }
            else // if (methodType == "PUT")
            {
                RecurringJob.AddOrUpdate($"dynamic-job-{Guid.NewGuid()}", () => Schedular.SendHttpPutRequest(url, requestBody), Cron.Minutely());
                return Ok($"Job scheduled to run daily at {executionDateTime} for URL: {url}");
            }
        }


        //[HttpPost("Schedule")]
        //public IActionResult Schedule(string url)
        //{
        //    _backgroundJobClient.Schedule(() => Console.Write("The url is "+url), TimeSpan.FromMinutes(2));
        //    return Ok();
        //}


        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {

          


            var parentJobId = _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobTestService.ContinuationJob());

            return Ok();
        }
    }
}

public class ScheduledJobModel
{
    public int JobId { get; set; }
    public int TypeId { get; set; }
    public string? Url { get; set; }
    public string? Frequency { get; set; }
    public string? ScheduleTime { get; set; }
}
