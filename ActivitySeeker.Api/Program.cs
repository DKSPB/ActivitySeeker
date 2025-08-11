using System.Text.Json.Serialization;
using NLog;
using NLog.Web;
using UseCases.DI;
using DataAccess.DI;
using FileSystem.DI;
using ActivitySeeker.Api.Extensions;
using Controllers.DI;
using Microsoft.AspNetCore.HttpOverrides;

namespace ActivitySeeker.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Info("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                
                builder.Services.AddControllers()
                    .AddJsonOptions(opt => 
                        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
                
                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddFileSystemInfrastructure(builder.Configuration);
                builder.Services.AddApplicationServices();
                builder.Services.AddControllersServices();
                builder.Services.AddSwaggerGenConfiguration();

                var app = builder.Build();
                
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
                
                app.UseSwaggerUiConfiguration(app.Environment);

                app.UseMiddleware<ExceptionHandlingMiddleware>();

                app.UseDefaultFiles();
                app.UseStaticFiles();

                app.UseRouting();
                app.MapControllers();
                
                app.MapFallbackToFile("index.html");

                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}


