using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Telegram.Bot;

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

            builder.Services.AddScoped<ActivitySeekerContext>();
            builder.Services.AddScoped<MessageHandler>();
            builder.Services.AddHttpClient("telegram_bot_client").AddTypedClient<ITelegramBotClient>(httpClient =>
            {
                TelegramBotClientOptions options = new(botConfiguration.BotToken);
                return new TelegramBotClient(options, httpClient);
            });
            
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<MessageHandler>();
            builder.Services.AddHostedService<ConfigureWebhook>();

            #region настройки сериализатора

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new StringEnumConverter(new SnakeCaseNamingStrategy()));
            serializerSettings.Formatting = Formatting.Indented;
            serializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(true, true),
            };
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            serializerSettings.NullValueHandling = NullValueHandling.Include;
            //serializerSettings.DateFormatString = "dd.MM.yyyy HH:mm";

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = serializerSettings.ContractResolver;
                    options.SerializerSettings.Converters = serializerSettings.Converters;
                    options.SerializerSettings.MissingMemberHandling = serializerSettings.MissingMemberHandling;
                    options.SerializerSettings.NullValueHandling = serializerSettings.NullValueHandling;
                    options.SerializerSettings.DateFormatString = serializerSettings.DateFormatString;
                });
            builder.Services.AddSingleton(serializerSettings);

            JsonConvert.DefaultSettings = () => serializerSettings;

            #endregion

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
    }
}


