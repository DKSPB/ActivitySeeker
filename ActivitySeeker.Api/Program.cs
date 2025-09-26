using NLog;
using Auth.DI;
using NLog.Web;
using UseCases.DI;
using FileSystem.DI;
using DataAccess.DI;
using Controllers.DI;
using UseCases.Interfaces.Auth;
using System.Text.Json.Serialization;
using ActivitySeeker.Api.Extensions.Auth;
using ActivitySeeker.Api.Extensions.Cors;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication;
using ActivitySeeker.Api.Extensions.Swagger;
using ActivitySeeker.Api.Extensions.Exceptions;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddConfigureCors(builder.Configuration, builder.Environment);

    builder.Services
        .AddAuthentication("VkScheme")
        .AddScheme<AuthenticationSchemeOptions, VkAuthenticationHandler>("VkScheme", _ => { });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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

    app.UseConfiguredCors();

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