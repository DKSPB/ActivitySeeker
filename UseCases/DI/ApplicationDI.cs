using UseCases.Activity.Commands.Create;
using UseCases.Activity.Commands.Update;
using UseCases.ActivityType.Commands.Create;
using Microsoft.Extensions.DependencyInjection;

namespace UseCases.DI;

public static class Infrastructure
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UseCaseMappingProfile).Assembly);
        
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(CreateActivityHandler).Assembly));
        
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(UpdateActivityCommand).Assembly));
        
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(CreateActivityTypeHandler).Assembly));
        
        return services;
    }
}