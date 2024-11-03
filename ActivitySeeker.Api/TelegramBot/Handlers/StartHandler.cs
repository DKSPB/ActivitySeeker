using ActivitySeeker.Bll.Models;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler: IHandler
{
    private const string MessageText = $"����� ������� ������������� ���� ������� ��� �����." +
                                   $"\n���� ��� ����� �� ������ � �����-���������, ������� ��� �������� ��� ����� ���������" +
                                   $"\n�� ������ ������� �������� ��� ��������� � �������:" +
                                   $"\n���� > ������� �����";

    private readonly ICityService _cityService;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public StartHandler(ICityService cityService, IUserService userService, ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _cityService = cityService;
        _activityPublisher = activityPublisher;
    }
    
    public async Task HandleAsync(UserDto currentUser, UserUpdate userData)
    {
        Message message;

        if(currentUser.CityId is not null)
        {
            message = await _activityPublisher.SendMessageAsync(
                userData.ChatId,
                currentUser.State.ToString(),
                null,
                Keyboards.GetMainMenuKeyboard());

            currentUser.State.StateNumber = StatesEnum.MainMenu;
        }
        else
        {
            var mskId = (await _cityService.GetCitiesByName("������")).First().Id;

            var spbId = (await _cityService.GetCitiesByName("�����-���������")).First().Id;

            message = await _activityPublisher.SendMessageAsync(
                userData.ChatId,
                MessageText,
                null,
                Keyboards.GetDefaultSettingsKeyboard(mskId, spbId));
            
            currentUser.State.StateNumber = StatesEnum.SaveDefaultSettings;
        }

        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}