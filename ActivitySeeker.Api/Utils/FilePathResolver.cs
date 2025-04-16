using ActivitySeeker.Api.Models;
using ActivitySeeker.UseCases.ActivityType.Dto;
using ActivitySeeker.UseCases.Utils;
using AutoMapper;

namespace ActivitySeeker.Api.Utils;

public class FilePathResolver : IValueResolver<ActivityTypeImageVM,  UploadActivityTypeImageDto, string>
{
    public string Resolve(ActivityTypeImageVM source, UploadActivityTypeImageDto destination, string destMember,
        ResolutionContext context)
    {
        return FileProvider.CombinePathToFile();
    }
}