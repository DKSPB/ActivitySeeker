using System.Reflection;
using ActivitySeeker.UseCases.Interfaces;
using ActivitySeeker.UseCases.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace ActivitySeeker.UseCases.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
        serviceCollection.AddScoped<IJwtProvider, JwtProvider>();
        
        serviceCollection.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return serviceCollection;
    }
}