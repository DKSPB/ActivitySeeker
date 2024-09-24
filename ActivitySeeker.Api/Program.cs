using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Api.TelegramBot;
using Microsoft.EntityFrameworkCore;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Services;
using ActivitySeeker.Domain;
using FluentValidation;

using Telegram.Bot;
using Microsoft.AspNetCore.HttpOverrides;

namespace ActivitySeeker.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
            builder.Services.Configure<BotConfiguration>(botConfigurationSection);
            
            var botConfiguration = botConfigurationSection.Get<BotConfiguration>();
            
            var connection = builder.Configuration.GetConnectionString("ActivitySeekerConnection");
            builder.Services.AddDbContext<ActivitySeekerContext>(options => options.UseNpgsql(connection));
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddScoped<ActivitySeekerContext>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();
            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddScoped<StartHandler>();
            builder.Services.AddScoped<MainMenuHandler>();
            builder.Services.AddScoped<ListOfActivitiesHandler>();
            builder.Services.AddScoped<SelectActivityTypeHandler>();
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

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseExceptionHandler("/ErrorHandling/ProcessError");
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
    
    public class BotConfiguration
    {
        public static readonly string Configuration = "BotConfiguration";
        public string BotToken { get; set; } = default!;

        public string WebhookUrl { get; set; } = default!;

        public string? PathToCertificate { get; set; }
    }
}


