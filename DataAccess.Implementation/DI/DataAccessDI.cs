namespace DataAccess.DI
{
    using Implementations;
    using Interfaces;
    using Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using UseCases.Interfaces.Repos;
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
            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
