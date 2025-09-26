using ActivitySeeker.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Implementations;

namespace IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public CustomWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Убираем реальный DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ActivitySeekerContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            // Добавляем DbContext для Postgres (TestContainers)
            services.AddDbContext<ActivitySeekerContext>(options =>
                options.UseNpgsql(_connectionString));
            
            // Прогоняем миграции при старте теста
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DbContext>();
            db.Database.Migrate();
        });
    }
}