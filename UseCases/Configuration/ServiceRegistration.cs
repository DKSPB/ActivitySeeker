using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ActivitySeeker.Bll.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return serviceCollection;
    }
}