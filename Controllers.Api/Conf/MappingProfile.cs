using AutoMapper;
using Domain.Entities;
using ActivitySeeker.UseCases.ActivityType.Dto;
using Controllers.Api.Models;
using Controllers.Api.Utils;
using ActivityTypeViewModel = ActivitySeeker.UseCases.ActivityType.Dto.ActivityTypeViewModel;

namespace Controllers.Api.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ActivityType, ActivityTypeViewModel>()
            .ForMember(x => x.ParentTypeName, 
                opt => 
                opt.MapFrom(src => src.Parent != null ? src.Parent.TypeName : null));
        
        CreateMap<CreateActivityTypeDto, ActivityType>();
        CreateMap<UpdateActivityTypeDto, ActivityType>();
        CreateMap<ActivityTypeDto, UpdateActivityTypeDto>();
        
        CreateMap<ActivityTypeImageVM, UploadActivityTypeImageDto>()
            .ForMember(dest => dest.Image,
                opt => 
                    opt.MapFrom(src => src.File.OpenReadStream()))
            .ForMember(dest => dest.Path,
                opt => opt.MapFrom<FilePathResolver>());
    }
}