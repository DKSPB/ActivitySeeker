using MediatR;
using UseCases.Common;
using FluentValidation;
using UseCases.Extensions;
using UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;


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
        
        services.AddScoped<IDateTimeConverter, DateTimeConverter>();

        return services;
    }
}