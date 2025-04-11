using AutoMapper;
using Domain.Entities;
using ActivitySeeker.UseCases.ActivityType.Dto;

namespace ActivitySeeker.Api.MappingProfile;

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
    }
}