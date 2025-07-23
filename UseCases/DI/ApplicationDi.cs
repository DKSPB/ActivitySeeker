using UseCases.Activity.Commands.Create;
using UseCases.Activity.Commands.Update;
using UseCases.ActivityType.Commands.Create;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Activity.Commands.Delete;
using UseCases.Activity.Queries.GetAll;
using UseCases.ActivityType.Commands.Delete;

namespace UseCases.DI;

public static class ApplicationDi
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UseCaseMappingProfile).Assembly);

        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(GetActivitiesHandler).Assembly));
        
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(CreateActivityHandler).Assembly));
        
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(UpdateActivityCommand).Assembly));
        
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(CreateActivityTypeHandler).Assembly));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DeleteActivityTypeCommand).Assembly));
        
        return services;
    }
}