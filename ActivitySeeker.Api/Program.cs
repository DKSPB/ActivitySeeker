using System.Text;
using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Api.TelegramBot;
using Microsoft.EntityFrameworkCore;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Services;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NLog;
using NLog.Web;
using Telegram.Bot;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ActivitySeeker.Bll.Notification;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

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

                var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
                builder.Services.Configure<BotConfiguration>(botConfigurationSection);

                var botConfiguration = botConfigurationSection.Get<BotConfiguration>();
                var connection = builder.Configuration.GetConnectionString("ActivitySeekerConnection");

                var jwtConfigurationSection = builder.Configuration.GetSection(nameof(JwtOptions));
                builder.Services.Configure<JwtOptions>(jwtConfigurationSection);
                var jwtOptions = jwtConfigurationSection.Get<JwtOptions>();
                
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                        };
                    });

                builder.Services.AddAuthorization();
                builder.Services.AddSignalR();
                builder.Services.AddDbContext<ActivitySeekerContext>(options => options.UseNpgsql(connection));
                builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
                builder.Services.AddScoped<ActivitySeekerContext>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<ActivityPublisher>();
                builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();
                builder.Services.AddScoped<IActivityService, ActivityService>();
                builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
                builder.Services.AddScoped<IJwtProvider, JwtProvider>();
                builder.Services.AddScoped<IAdminService, AdminService>();
                builder.Services.AddScoped<ICityService, CityService>();
                builder.Services.AddScoped<SetDefaultSettingsHandler>();
                builder.Services.AddScoped<StartHandler>();
                builder.Services.AddScoped<MainMenuHandler>();
                builder.Services.AddScoped<ListOfActivitiesHandler>();
                builder.Services.AddScoped<ListOfChildrenActivitiesHandler>();
                builder.Services.AddScoped<SelectActivityTypeHandler>();
                builder.Services.AddScoped<SaveActivityFormatHandler>();
                builder.Services.AddScoped<SelectActivityFormat>();
                builder.Services.AddScoped<SaveOfferFormat>();
                builder.Services.AddScoped<SelectOfferCity>();
                builder.Services.AddScoped<SelectActivityPeriodHandler>();
                builder.Services.AddScoped<SelectTodayPeriodHandler>();
                builder.Services.AddScoped<SelectTomorrowPeriodHandler>();
                builder.Services.AddScoped<SelectAfterTomorrowPeriodHandler>();
                builder.Services.AddScoped<SelectWeekPeriodHandler>();
                builder.Services.AddScoped<SelectMonthPeriodHandler>();
                builder.Services.AddScoped<SelectUserPeriodHandler>();
                builder.Services.AddScoped<UserSetFromDateHandler>();
                builder.Services.AddScoped<UserSetByDateHandler>();
                builder.Services.AddScoped<SearchResultHandler>();
                builder.Services.AddScoped<PreviousHandler>();
                builder.Services.AddScoped<NextHandler>();
                builder.Services.AddScoped<OfferHandler>();
                builder.Services.AddScoped<SaveOfferDateHandler>();
                builder.Services.AddScoped<ConfirmOfferHandler>();
                builder.Services.AddScoped<AddOfferDescriptionHandler>();
                builder.Services.AddScoped<SaveOfferDescriptionHandler>();
                builder.Services.AddScoped<SaveDefaultSettingsHandler>();
                builder.Services.AddSingleton<NotificationAdminHub>();
                

                builder.Services.AddHttpClient("telegram_bot_client").AddTypedClient<ITelegramBotClient>(httpClient =>
                {
                    TelegramBotClientOptions options = new(botConfiguration.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

                builder.Services.AddHttpClient();
                builder.Services.AddHostedService<ConfigureWebhook>();

                #region serialize settings

                builder.Services.AddControllers().AddNewtonsoftJson();

                #endregion

                builder.Services.AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                    });
                    
                    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
                });
                
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                var app = builder.Build();

                var supportedCultures = new[]
                {
                    new CultureInfo("ru-RU"),
                    new CultureInfo("ru"),
                    new CultureInfo("en-US")
                };

                app.UseRequestLocalization(new RequestLocalizationOptions 
                {
                    DefaultRequestCulture = new RequestCulture("ru-RU"),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                });

                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseExceptionHandler("/ErrorHandling/ProcessError");

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
                }

                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                app.MapHub<NotificationAdminHub>("/notify");

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
    
    public class BotConfiguration
    {
        public static readonly string Configuration = "BotConfiguration";
        public string BotToken { get; set; } = default!;

        public string WebhookUrl { get; set; } = default!;

        public string? PathToCertificate { get; set; }

        public string TelegramChannel { get; set; } = default!;
    }
}


