using AutoMapper;
using UseCases.Activity.Commands.Create;
using UseCases.ActivityType.Commands.Create;
using ActivityEntity = Domain.Entities.Activity;
using ActivityTypeEntity = Domain.Entities.ActivityType;

namespace UseCases.DI;

public class UseCaseMappingProfile : Profile
{
    public UseCaseMappingProfile()
    {
        CreateMap<CreateActivityCommand, ActivityEntity>();
        CreateMap<CreateActivityTypeCommand, ActivityTypeEntity>();
    }
}