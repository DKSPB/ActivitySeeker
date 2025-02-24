using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SelectOfferCity)]
public class SelectOfferCity : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    private readonly ICityService _cityService;
    private int _mskId = -1;
    private int _spbId = -1;
    private List<City> _cities = new ();

    public SelectOfferCity(
        ICityService cityService, 
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) 
        : base(userService, activityService, activityPublisher)
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

            _cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            if (!_cities.Any())
            {
                Response.Text = $"Поиск не дал результата." +
                                      $"\nУточните название и попробуйте ещё раз.";

                Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
                Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(_mskId, _spbId, false);

                _mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;
                _spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;
            }
            else
            {
                Response.Text = $"Найденные города:";
                Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
                Response.Keyboard = Keyboards.GetCityKeyboard(_cities);
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

            if (city is null && cityId != -1)
            {
                throw new NullReferenceException("По заданному идентификатору городов не обнаружено");
            }

            if(cityId == -1)
            {
                CurrentUser.Offer.CityId = CurrentUser.CityId;
            }
            else if (cityId != -1)
            {
                CurrentUser.CityId = cityId;
                CurrentUser.Offer.CityId = cityId;
            }

            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
            Response.Text = "Заполни описание активности:";
            Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
            Response.Keyboard = Keyboards.GetEmptyKeyboard(); 
        }
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}