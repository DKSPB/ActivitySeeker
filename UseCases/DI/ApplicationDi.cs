using MediatR;
using UseCases.Common;
using FluentValidation;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using MediatR.Behaviors.Authorization.Interfaces;
using UseCases.Extensions;
using UseCases.Interfaces.Common;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Activity.Commands.Update;
using UseCases.Extensions.Authorization;


namespace UseCases.DI;

public static class ApplicationDi
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UseCaseMappingProfile).Assembly);
        
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
        
        services.AddMediatorAuthorization(typeof(UserMustAuthorActivityRequirement).Assembly);
        services.AddAuthorizersFromAssembly(typeof(UpdateActivityCommandAuthorizer).Assembly);
        
        //services.AddTransient(typeof(IAuthorizer<>), typeof(GlobalAllowAuthorizer<>));
        
        services.AddScoped<IDateTimeConverter, DateTimeConverter>();

        return services;
    }
}