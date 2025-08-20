using NLog;
using NLog.Web;
using UseCases.DI;
using Auth.DI;
using DataAccess.DI;
using FileSystem.DI;
using Controllers.DI;
using ActivitySeeker.Api.Extensions;
using System.Text.Json.Serialization;
using ActivitySeeker.Api.Auth;
using Microsoft.AspNetCore.Authentication;
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
                
                builder.Services
                    .AddAuthentication("VkScheme")
                    .AddScheme<AuthenticationSchemeOptions, VkAuthenticationHandler>("VkScheme", options => { });
                
                builder.Services.AddCors(options =>
                {
                   options.AddPolicy("VkMiniAppsPolicy", policy =>
                   {
                       policy.WithOrigins("http://localhost:5173", "https://user147945853-saxd7mr3.tunnel.vk-apps.com/")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                   }); 
                });

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                
                builder.Services.AddControllers()
                    .AddJsonOptions(opt => 
                        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
                
                builder.Services.AddDataAccessInfrastructure(builder.Configuration);
                builder.Services.AddAuthInfrastructure(builder.Configuration);
                builder.Services.AddFileSystemInfrastructure(builder.Configuration);
                builder.Services.AddApplicationServices();
                builder.Services.AddControllersServices();
                builder.Services.AddSwaggerGenConfiguration();

                var app = builder.Build();
                
                app.UseCors("VkMiniAppsPolicy");
                
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


