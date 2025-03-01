using ActivitySeeker.Bll.Models;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;
using ActivitySeeker.Bll.Utils;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Offer)]
public class OfferHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    private readonly IActivityTypeService _activityTypeService;
    private IEnumerable<ActivityTypeDto>? _children;

    public OfferHandler (
        IUserService userService, 
        IActivityService activityService, 
        IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) 
        : base(userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;
        _activityTypeService = activityTypeService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.Offer;
        var activityTypeIdParseResult = Guid.TryParse(userData.Data, out var activityTypeId);

        if (!activityTypeIdParseResult)
        {
            var activityTypes = (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
            Response.Text = "Выбери тип активности:";
            Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
            Response.Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName(), "Меню");
        }
        else
        {
            var activityType = await _activityTypeService.GetById(activityTypeId);

            _children = (await _activityTypeService.GetAll())
            .Where(x => x.ParentId == activityType.Id).ToList();

            if (!_children.Any())
            {
                Response.Text = "Выбери формат проведения:";
                CurrentUser.State.StateNumber = StatesEnum.SaveOfferFormat;
                Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
                Response.Keyboard = Keyboards.GetActivityFormatsKeyboard(false);

                CreateOfferIfNotExists((Guid)activityType.Id);
            }
            else
            {
                Response.Text = "Выбери тип активности: ";

                if (activityType.ParentId is null)
                {
                    Response.Image = activityType.ImagePath is null ? null : await GetImage(activityType.ImagePath);
                    Response.Keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList(), StatesEnum.Offer.GetDisplayName());
                }
                else 
                {
                    Response.Image = activityType.ImagePath is null ? null : await GetImage(activityType.ImagePath);
                    Response.Keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList(), activityType.ParentId.ToString());
                }
                
            }
        }
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
    private void CreateOfferIfNotExists(Guid activityTypeId)
    {
        if (CurrentUser.Offer is null)
        {
            CurrentUser.Offer = new ActivityDto()
            {
                ActivityTypeId = activityTypeId,
                LinkOrDescription = string.Empty,
                OfferState = null,
                IsOnline = false
            };
        }
        else
        {
            CurrentUser.Offer.ActivityTypeId = activityTypeId;
        }
    }
}