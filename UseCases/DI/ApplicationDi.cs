using Microsoft.Extensions.DependencyInjection;


namespace UseCases.DI;

public static class ApplicationDi
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UseCaseMappingProfile).Assembly);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));

        return services;
    }
}