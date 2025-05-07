using ApplicationServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Implementation.Conf;

public static class ApplicationServiceConf
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IActivityTypeService, ActivityTypeService>();
        serviceCollection.AddScoped<IActivityService, ActivityService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ICityService, CityService>();
        serviceCollection.AddScoped<IAdminService, AdminService>();
        
        return serviceCollection;
    }
}