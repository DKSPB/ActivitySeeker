using Microsoft.Extensions.DependencyInjection;

namespace Controllers.DI;

public static class ControllersDi
{
    public static IServiceCollection AddControllersServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ControllersMappingProfile).Assembly);
        return services;
    }
}