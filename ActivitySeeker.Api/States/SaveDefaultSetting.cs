using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.States
{
    public class SaveDefaultSetting
    {
        private const StatesEnum CurrentState = StatesEnum.SaveDefaultSettings;

        private const string MessageText = $"Перед началом использования бота выберите Ваш Город." +
                               $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название, как текст сообщения" +
                               $"\nВы всегда сможете изменить Ваш город в разделе:" +
                               $"\nМеню > Выбрать город";

        private readonly string _rootImageFolder;
        private readonly string _webRootPath;

        public SaveDefaultSetting(string rootImageFolder, string webRootPath)
        {
            _rootImageFolder = rootImageFolder;
            _webRootPath = webRootPath;
        }

        public async Task<ResponseMessage> GetResponseMessage(int mskId, int spbId, bool withSkip)
        {
            return new ResponseMessage
            {
                Text = MessageText,
                Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, withSkip),
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
