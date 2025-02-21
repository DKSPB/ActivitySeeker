using System.Text;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.SetDefaultSettings*/"/city")]
public class SetDefaultSettingsHandler: AbstractHandler
{
    private readonly ICityService _cityService;
    private readonly string _webRootPath;
    private readonly string _rootImageFolder;

    public SetDefaultSettingsHandler(
        ICityService cityService, 
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions)
        : base(userService, activityService, activityPublisher)
    {
        _cityService = cityService;
        _webRootPath = webHostEnvironment.WebRootPath;
        _rootImageFolder = botConfigOptions.Value.RootImageFolder;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var nextState = StatesEnum.SaveDefaultSettings;
        CurrentUser.State.StateNumber = nextState;
        
        var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

        var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

        var currentCity = "не задан";

        if (CurrentUser.CityId.HasValue)
        {
            var userCity = await _cityService.GetById(CurrentUser.CityId.Value);

            if (userCity is not null)
            {
                currentCity = userCity.Name;
            }
        }

        Response.Text = CreateResponseMessage(currentCity);
        Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);
        Response.Image = await GetImage(nextState.ToString());

        
    }

    private string CreateResponseMessage(string currentCity)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Задайте Ваш Город.");
        stringBuilder.AppendLine("Если Ваш город не Москва и Санкт-Петербург, введите его название как текст сообщения.");
        stringBuilder.AppendLine($"Текущий город: {currentCity}.");
        return stringBuilder.ToString();
    }
    
    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _rootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}