using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.States
{
    public class ActivityFormatChapter
    {
        private const StatesEnum CurrentState = StatesEnum.SelectActivityFormat;
        private const string MessageText = "Выбери формат проведения активности:";

        private readonly string _rootImageFolder;
        private readonly string _webRootPath;

        public ActivityFormatChapter(string rootImageFolder, string webRootPath)
        {
            _rootImageFolder = rootImageFolder;
            _webRootPath = webRootPath;
        }

        public async Task<ResponseMessage> GetResponseMessage(bool withAny)
        {
            return new ResponseMessage
            {
                Text = MessageText,
                Keyboard = Keyboards.GetActivityFormatsKeyboard(withAny),
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
