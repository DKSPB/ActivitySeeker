using AutoMapper;
using Controllers.Models;
using UseCases.Activity.Commands.UploadImage;
using UseCases.Common;

namespace Controllers.DI;

public class ControllersMappingProfile : Profile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UploadActivityImage, UploadActivityImageCommand>()
                .ConvertUsing(src => new UploadActivityImageCommand(
                    src.ActivityId,
                    new InputFile(
                        src.File.OpenReadStream(),
                        src.File.FileName
                    )
                ));
        }
    }
}