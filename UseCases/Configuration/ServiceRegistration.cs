using System.Reflection;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ActivitySeeker.UseCases.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IActivityTypeService, ActivityTypeService>();
        
        serviceCollection.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return serviceCollection;
    }
}