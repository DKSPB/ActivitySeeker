using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ListOfActivities)]
public class ListOfActivitiesHandler: AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private List<ActivityTypeDto> _childrenTypes;

    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;

    public ListOfActivitiesHandler(IUserService userService,
        IActivityService activityService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) :
        base(userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;

        _childrenTypes = new List<ActivityTypeDto>();
        _activityTypeService = activityTypeService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var activityTypeIdParseResult = Guid.TryParse(userData.Data, out var selectedActivityId);

        if (!activityTypeIdParseResult)
        {
            //var nextState = StatesEnum.ListOfActivities;
            CurrentUser.State.StateNumber = StatesEnum.ListOfActivities;//nextState;

            var activityTypes = (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
            activityTypes.Insert(0, new ActivityTypeDto { Id = Guid.Empty, TypeName = "Все виды активностей" });

            var listOfActivitiesState = new ListOfActivities(_botConfig.RootImageFolder, _webRootPath);
            Response = await listOfActivitiesState.GetResponseMessage(activityTypes, StatesEnum.MainMenu.ToString());
            /*Response.Text = "Выбери тип активности:";
            Response.Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName());
            Response.Image = await GetImage(nextState.ToString());*/
        }
        else
        {
            if (selectedActivityId == Guid.Empty)
            {
                CurrentUser.State.ActivityType = new();

                //var nextState = StatesEnum.MainMenu;
                CurrentUser.State.StateNumber = StatesEnum.MainMenu;//nextState;

                var mainMenuState = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
                Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
                /*Response.Text = CurrentUser.State.ToString();
                Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                Response.Image = await GetImage(nextState.ToString());*/
                
            }
            else
            {
                var selectedActivityType = await _activityTypeService.GetById(selectedActivityId);

                _childrenTypes = (await _activityTypeService.GetAll())
                    .Where(x => x.ParentId == selectedActivityType.Id).ToList();

                if (!_childrenTypes.Any())
                {
                    //var nextState = StatesEnum.MainMenu;
                    CurrentUser.State.StateNumber = StatesEnum.MainMenu;//nextState;

                    CurrentUser.State.ActivityType.Id = selectedActivityType.Id;
                    CurrentUser.State.ActivityType.TypeName = selectedActivityType.TypeName;

                    var mainMenuState = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
                    Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
                    /*Response.Text = CurrentUser.State.ToString();
                    Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                    Response.Image = await GetImage(nextState.ToString());*/
                }
                else
                {
                    if (CurrentUser.State.ActivityType.Id == selectedActivityType.Id && CurrentUser.State.StateNumber == StatesEnum.ListOfChildrenActivities)
                    {
                        //var nextState = StatesEnum.MainMenu;
                        CurrentUser.State.StateNumber = StatesEnum.MainMenu;//nextState;

                        var mainMenuState = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
                        Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
                        /*Response.Text = CurrentUser.State.ToString();
                        Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                        Response.Image = selectedActivityType.ImagePath is null ? null : await GetImage(nextState.ToString());*/
                    }
                    else
                    {
                        CurrentUser.State.StateNumber = StatesEnum.ListOfChildrenActivities;

                        _childrenTypes.Add(new ActivityTypeDto { Id = selectedActivityType.Id, TypeName = $"Выбрать все {selectedActivityType.TypeName}" });

                        var backButtonValue = selectedActivityType.ParentId is null ? StatesEnum.ListOfActivities.GetDisplayName() : selectedActivityType.ParentId.ToString();
                        
                        var imageName = selectedActivityType.ImagePath is null ? null : selectedActivityType.ImagePath;

                        var listOfChildrenActivitiesState = new ListOfChildrenActivities(_botConfig.RootImageFolder, _webRootPath);
                        Response = await listOfChildrenActivitiesState.GetResponseMessage(_childrenTypes.ToList(), backButtonValue, imageName);

                        /*Response.Text = "Выбери тип активности:";
                        Response.Keyboard = Keyboards.GetActivityTypesKeyboard(_childrenTypes.ToList(), backButtonValue);
                        Response.Image = selectedActivityType.ImagePath is null ? null : await GetImage(selectedActivityType.ImagePath);*/

                        CurrentUser.State.ActivityType.Id = selectedActivityType.Id;
                        CurrentUser.State.ActivityType.TypeName = selectedActivityType.TypeName;
                    }
                }
            }
        }
    }

    /*private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }*/
}