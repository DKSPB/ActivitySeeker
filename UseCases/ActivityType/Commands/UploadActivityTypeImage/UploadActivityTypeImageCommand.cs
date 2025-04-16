using MediatR;
using ActivitySeeker.UseCases.ActivityType.Dto;

namespace ActivitySeeker.UseCases.ActivityType.Commands.UploadActivityTypeImage;

public class UploadActivityTypeImageCommand : IRequest
{
    public UploadActivityTypeImageCommand(UploadActivityTypeImageDto activityTypeImage)
    {
        UploadActivityTypeImage = activityTypeImage;
    }
    
    public UploadActivityTypeImageDto UploadActivityTypeImage { get; }
}