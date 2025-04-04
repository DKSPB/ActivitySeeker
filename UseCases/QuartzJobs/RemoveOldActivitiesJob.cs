using ActivitySeeker.Bll.Interfaces;
using Quartz;

namespace ActivitySeeker.Bll.QuartzJobs;

public class RemoveOldActivitiesJob : IJob
{
    private readonly IActivityService _activityService;

    public RemoveOldActivitiesJob(IActivityService activityService)
    {
        _activityService = activityService;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        await _activityService.RemoveOldActivities();
    }
}