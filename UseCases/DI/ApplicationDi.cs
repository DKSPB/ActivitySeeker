using Microsoft.Extensions.DependencyInjection;
using UseCases.Common;
using UseCases.Interfaces;


namespace UseCases.DI;

public static class ApplicationDi
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UseCaseMappingProfile).Assembly);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
        
        services.AddScoped<IDateTimeConverter, DateTimeConverter>();

        return services;
    }
}