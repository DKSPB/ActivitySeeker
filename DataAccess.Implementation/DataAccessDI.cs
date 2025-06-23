using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseOptions>(config.GetSection(DatabaseOptions.SectionName));

            services.AddDbContext<ActivitySeekerContext>((sp, options) =>
            {
                var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseSqlServer(dbOptions.ConnectionString);
            });

            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<ActivitySeekerContext>());
            return services;
        }
    }
}
