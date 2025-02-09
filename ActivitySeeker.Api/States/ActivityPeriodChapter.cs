using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.States
{
    public class ActivityPeriodChapter
    {
        private const StatesEnum CurrentState = StatesEnum.ActivityPeriodChapter;
        private const string MessageText = "Выбери период проведения активности:";

        private readonly string _rootImageFolder;
        private readonly string _webRootPath;

        public ActivityPeriodChapter(string rootImageFolder, string webRootPath)
        {
            _rootImageFolder = rootImageFolder;
            _webRootPath = webRootPath;
        }

        public async Task<ResponseMessage> GetResponseMessage()
        {
            return new ResponseMessage
            {
                Text = MessageText,
                Keyboard = Keyboards.GetPeriodActivityKeyboard(),
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
