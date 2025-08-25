using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Interfaces;

namespace Auth.DI
{
    public static class AuthDi
    {
        public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<VkSecretKeyOption>(config.GetSection(VkSecretKeyOption.SectionName));

            services.AddTransient<ILaunchParamsValidator, VkLaunchParamsValidator>();

            services.AddScoped<IEntityAuthorization<Activity, Guid>, ActivityAuthorization>();

            return services;
        }
    }
}
