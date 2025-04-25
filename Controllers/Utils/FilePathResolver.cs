using ActivitySeeker.UseCases.ActivityType.Dto;
using AutoMapper;
using Controllers.Models;

namespace Controllers.Utils;

public class FilePathResolver : IValueResolver<ActivityTypeImageVM,  UploadActivityTypeImageDto, string>
{
    private readonly IFilePathGenerator _filePathGenerator;
    public FilePathResolver(IFilePathGenerator filePathGenerator)
    {
        _filePathGenerator = filePathGenerator;
    }
    public string Resolve(ActivityTypeImageVM source, UploadActivityTypeImageDto destination, string destMember,
        ResolutionContext context)
    {
        return _filePathGenerator.GeneratePath();
    }
}