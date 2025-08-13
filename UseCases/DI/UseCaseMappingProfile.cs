using AutoMapper;
using UseCases.Activity.Commands.Create;
using UseCases.Activity.Models;
using UseCases.ActivityType.Commands.Create;
using UseCases.ActivityType.Models;
using ActivityEntity = Domain.Entities.Activity;
using ActivityTypeEntity = Domain.Entities.ActivityType;

namespace UseCases.DI;

public class UseCaseMappingProfile : Profile
{
    public UseCaseMappingProfile()
    {
        CreateMap<ActivityTypeEntity, ActivityTypeDto>();
        
        CreateMap<ActivityEntity, ActivityDto>()
            .ForMember(dto => dto.ActivityType, 
                conf => 
                    conf.MapFrom(entity => entity.ActivityType.TypeName));
        
        

        CreateMap<CreateActivityCommand, ActivityEntity>();

        CreateMap<CreateActivityTypeCommand, ActivityTypeEntity>();
    }
}