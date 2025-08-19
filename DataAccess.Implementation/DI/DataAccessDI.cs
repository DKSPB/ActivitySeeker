using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DataAccess.DI

{
    public static class Infrastructure
    {
        public static IServiceCollection AddDataAccessInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseOptions>(config.GetSection(DatabaseOptions.SectionName));
            
            services.AddDbContext<ActivitySeekerContext>((sp, options) =>
            {
                var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseNpgsql(dbOptions.ActivitySeekerConnection);
            });
            
            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<ActivitySeekerContext>());

            return services;
        }
    }
}
