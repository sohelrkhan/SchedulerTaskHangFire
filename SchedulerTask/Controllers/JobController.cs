using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;
using SchedulerTask.Services;

namespace SchedulerTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        [HttpGet]
        [Route("RunJobApi")]
        public ActionResult RunJobApi()
        {
            //Remove all previous RecurringJob
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            //Set TimeZone
            RecurringJobOptions timeZone = new()
            {
                TimeZone = TimeZoneInfo.Local
            };

            //Add Recurring Job
            RecurringJob.AddOrUpdate<JobService>("SchedulerTask", x => x.RunJobSchedulerTask(), "* * * * *", timeZone); 
            RecurringJob.AddOrUpdate<JobService>("SchedulerTask_JumpUp", x => x.RunJobSchedulerTask_JumpUp(), "*/2 * * * *", timeZone); 
            RecurringJob.AddOrUpdate<JobService>("SchedulerTask_ExecutivePartnerBenefits", x => x.RunJobSchedulerTask_ExecutivePartnerBenefits(), "0 0 * * *", timeZone);

            return Ok();
        }
    }
}


    
