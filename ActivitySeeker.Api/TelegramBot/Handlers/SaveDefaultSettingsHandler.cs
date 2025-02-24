using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveDefaultSettings)]
public class SaveDefaultSettingsHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    private readonly ICityService _cityService;

    public SaveDefaultSettingsHandler (
        ICityService cityService, 
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher publisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions)
        : base(userService, activityService, publisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (userData.CallbackQueryId is null)
        {
            var cityName = userData.Data;

            var cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            if (!cities.Any())
            {
                var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

                var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

                Response.Text = $"Поиск не дал результатов." +
                                      $"\nУточните название и попробуйте ещё раз";
                Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
                Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);

            }
            else
            {
                Response.Text = $"Найденные города:";
                Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
                Response.Keyboard = Keyboards.GetCityKeyboard(cities.ToList());
            }
        }
        else
        {
            var cityIdString = userData.Data;

            var parseResult = int.TryParse(cityIdString, out var cityId);

            if (!parseResult)
            {
                throw new ArgumentNullException($"Не удалось распознать идентификатор города: {cityIdString}");
            }

            var city = await _cityService.GetById(cityId);

            if (city is null)
            {
                throw new NullReferenceException("По заданному идентификатору городов не обнаружено");
            }

            CurrentUser.CityId = cityId;
            var nextState = StatesEnum.MainMenu;
            CurrentUser.State.StateNumber = nextState;

            Response.Text = CurrentUser.State.ToString();
            Response.Image = await GetImage(nextState.ToString());
            Response.Keyboard = Keyboards.GetMainMenuKeyboard();
        }
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}