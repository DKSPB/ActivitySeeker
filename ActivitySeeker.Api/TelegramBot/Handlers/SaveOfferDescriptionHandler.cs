using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDescription)]
public class SaveOfferDescriptionHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    public SaveOfferDescriptionHandler(
        IUserService userService,
        IActivityService activityService,
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions)
        :base(userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var offerDescription = userData.Data;

        if (CurrentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности, offer is null");
        }

        if (!string.IsNullOrWhiteSpace(offerDescription) && offerDescription.Length <= 2000)
        {
            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDate;
            CurrentUser.Offer.LinkOrDescription = offerDescription;

            Response.Text = $"Заполни дату и время проведения события в формате: (дд.мм.гггг чч.мм):" +
          $"\nПример:{DateTime.Now:dd.MM.yyyy HH:mm}";

            Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
        }
        else
        {
            Response.Text = "Описание события не может быть пустым, состоять только из пробелов и содержать больше 2000 символов";
            Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
        }
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}