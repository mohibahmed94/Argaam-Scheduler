using Hangfire.Server;
using RestSharp.Authenticators;
using RestSharp;
using ArgaamSchedular.Entities;
using System.Threading;
using Hangfire;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArgaamSchedular.Services
{
    public class JobTestService : IJobTestService
    {

        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public JobTestService(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }
        public void FireAndForgetJob()
        {

            Console.WriteLine("Hello from a Fire and Forget job!");
        }
        public void ReccuringJob(PerformContext context, List<ScheduledJob> jobs)
        {
            Console.WriteLine("Hello from a Scheduled job!");
        }
        public void DelayedJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
        }
        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }

        public void SheduledJob()
        {

            Console.WriteLine("Hello from a Scheduled job!");
        }

        //public void ReccuringJob(List<ExternalJob> jobs)
        //{


        //    foreach (var item in jobs)
        //    {

        //    }

        //    _recurringJobManager.AddOrUpdate("JobId", () => SheduledJob(), Cron.Minutely);


        //    //var options = new RestClientOptions("https://api.twitter.com/1.1")
        //    //{
        //    //    Authenticator = new HttpBasicAuthenticator("username", "password")
        //    //};
        //    //var client = new RestClient(options);

        //    //var request = new RestRequest("statuses/home_timeline.json");

        //    //// The cancellation token comes from the caller. You can still make a call without it.
        //    //var timeline = await client.Post(request);

        //}

        //public void ReccuringJob(List<ExternalJob> jobs)
        //{
        //    foreach (var item in jobs)
        //    {

        //    }

        //    _recurringJobManager.AddOrUpdate("JobId", () => SheduledJob(), Cron.Minutely);

        //    throw new NotImplementedException();
        //}

        //public void ReccuringJob(IEnumerable<ExternalJob> jobs)
        //{
        //    foreach (var item in jobs)
        //    {

        //    }

        //    _recurringJobManager.AddOrUpdate("JobId", () => SheduledJob(), Cron.Minutely);

        //    throw new NotImplementedException();
        //}



        public void ReccuringJob(IEnumerable<ScheduledJob> jobs)
        {
            throw new NotImplementedException();
        }

        public void ReccuringJob(Task<IEnumerable<ScheduledJob>> data)
        {
            var jobs = data.Result.ToList();
            foreach (var item in jobs)
            {
                int id = Convert.ToInt32(item.ScheduleTime);
                //Demo.SendHttpRequestWithDelayAsync(item.Url,id);
            }
        }
    }
}
