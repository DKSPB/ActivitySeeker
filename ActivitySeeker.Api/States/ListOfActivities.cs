using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.States
{
    public class ListOfActivities
    {
        private const string MessageText = @"Выбери тип активности:";
        private const StatesEnum CurrentState = StatesEnum.ListOfActivities;

        private readonly string _rootImageFolder;
        private readonly string _webRootPath;

        public ListOfActivities(string rootImageFolder, string webRootPath)
        {
            _rootImageFolder = rootImageFolder;
            _webRootPath = webRootPath;
        }

        public async Task<ResponseMessage> GetResponseMessage(List<ActivityTypeDto> activityTypes, string backButtonValue)
        {
            return new ResponseMessage
            {
                Text = MessageText,
                Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes,backButtonValue),
                Image = await GetImage(CurrentState.ToString())
            };
        }

        private async Task<byte[]?> GetImage(string fileName)
        {
            var filePath = FileProvider.CombinePathToFile(_webRootPath, _rootImageFolder, fileName);

            return await FileProvider.GetImage(filePath);
        }
    }
}
