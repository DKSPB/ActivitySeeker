using ActivitySeeker.UseCases.Interfaces;
using ApplicationServices.Interfaces;
using Quartz;

namespace ActivitySeeker.UseCases.QuartzJobs;

public class RemoveOldActivitiesJob : IJob
{
    private readonly IActivityService _activityService;

    public RemoveOldActivitiesJob(IActivityService activityService)
    {
        _activityService = activityService;
    }
    
    public /*async*/ Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask; //await _activityService.RemoveOldActivities();
    }
}