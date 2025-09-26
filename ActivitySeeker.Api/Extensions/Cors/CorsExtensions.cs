using System.Text.RegularExpressions;

namespace ActivitySeeker.Api.Extensions.Cors;

public static class CorsExtensions
{
    public static IServiceCollection AddConfigureCors(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        var corsSettings = configuration.GetSection("Cors").Get<CorsSettings>();

        services.AddCors(options =>
        {
            options.AddPolicy("VkMiniAppsPolicy", policy =>
            {
                if (environment.IsDevelopment())
                {
                    // В девелопе: разрешаем паттерны, т.к. используем тунели
                    policy.SetIsOriginAllowed(origin =>
                        {
                            if (corsSettings.AllowedOrigins.Contains(origin))
                                return true;

                            return corsSettings.AllowedPatternOrigins.Any(pattern =>
                                Regex.IsMatch(origin, pattern));
                        })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }
                else
                {
                    // В продакшене: только явные URL
                    policy.WithOrigins(corsSettings.AllowedOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }
            });
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        app.UseCors("VkMiniAppsPolicy");
        return app;
    }
}