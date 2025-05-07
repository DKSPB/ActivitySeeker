using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Controllers.Api.Conf;

public static class ControllersConf
{
    public static IServiceCollection RegisterControllers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(MappingProfile.MappingProfile).Assembly);

        serviceCollection.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return serviceCollection;
    }
}