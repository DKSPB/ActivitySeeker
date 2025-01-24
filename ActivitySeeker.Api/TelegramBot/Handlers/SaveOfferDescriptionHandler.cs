using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDescription)]
public class SaveOfferDescriptionHandler : AbstractHandler
{
    public SaveOfferDescriptionHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        var offerDescription = userData.Data;

        if (CurrentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности, offer is null");
        }

        if (!string.IsNullOrWhiteSpace(offerDescription) && offerDescription.Length <= 2000)
        {
            Response.Text = $"Заполни дату и время проведения события в формате: (дд.мм.гггг чч.мм):" +
                      $"\nПример:{DateTime.Now:dd.MM.yyyy HH:mm}";

            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDate;
            CurrentUser.Offer.LinkOrDescription = offerDescription;
        }
        else
        {
            Response.Text = "Описание события не может быть пустым, состоять только из пробелов и содержать больше 2000 символов";
        }

        return Task.CompletedTask;
    }
}