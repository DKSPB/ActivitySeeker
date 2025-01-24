using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferFormat)]
public class SaveOfferFormat: AbstractHandler
{
    private readonly ILogger<SaveOfferFormat> _logger;
    private readonly ICityService _cityService;
    private int _mskId = -1;
    private int _spbId = -1;
    private bool _withSkipButton;
    private string _userData = string.Empty;

    public SaveOfferFormat(ILogger<SaveOfferFormat> logger, IUserService userService, 
        IActivityService activityService, ActivityPublisher activityPublisher, ICityService cityService) 
        : base(userService, activityService, activityPublisher)
    {
        _logger = logger;
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        _userData = userData.Data;
        if (CurrentUser.Offer is null)
        {
            var msg = $"Объект CurrentUser.Offer = null";
            _logger.LogError(msg);
            throw new ArgumentNullException(msg);
        }

        if (_userData.Equals("online"))
        {
            CurrentUser.Offer.IsOnline = true;
            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
            Response.Text = $"Заполни описание события";
            Response.Keyboard = Keyboards.GetEmptyKeyboard();
        }
        
        else if(_userData.Equals("offline"))
        {
            CurrentUser.Offer.IsOnline = false;
            CurrentUser.State.StateNumber = StatesEnum.SelectOfferCity;
            
            _spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;
            _mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

            if (CurrentUser.CityId is not null)
            {
                _withSkipButton = true;
                Response.Text = $"Выберите город проведения активности" +
                                      $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название как текст сообщения" +
                                      $"\nНажмите кнопку \"Пропустить\", что бы оставить стандартные настройки города" +
                                      $"\nВаш город: {(await _cityService.GetById(CurrentUser.CityId.Value))?.Name}";
            }
            else
            {
                _withSkipButton = false;

                Response.Text = $"Выберите город проведения активности" +
                                      $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название как текст сообщения";
            }
            Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(_mskId, _spbId, _withSkipButton);
        }
        else
        {
            Response.Text = "Выберите формат проведения активности:";
            Response.Keyboard = Keyboards.GetActivityFormatsKeyboard(false);
        }
    }
}