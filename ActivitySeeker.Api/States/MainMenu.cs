using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.States
{
    public class MainMenu
    {
        private const StatesEnum CurrentState = StatesEnum.MainMenu;

        private readonly string _rootImageFolder;
        private readonly string _webRootPath;

        public MainMenu(string rootImageFolder, string webRootPath)
        {
            _rootImageFolder = rootImageFolder;
            _webRootPath = webRootPath;
        }

        public async Task<ResponseMessage> GetResponseMessage(string messageText) 
        {
            return new ResponseMessage
            {
                Text = messageText,
                Keyboard = Keyboards.GetMainMenuKeyboard(),
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
