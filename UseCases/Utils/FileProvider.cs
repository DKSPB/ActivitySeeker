
namespace ActivitySeeker.UseCases.Utils
{
    public static class FileProvider
    {
        public static string CombinePathToFile(string webRootPath, string rootImageFolder, string fileName)
        {
            return Path.Combine(webRootPath, rootImageFolder, fileName);
        }

        public static async Task UploadImage(string filePath, Stream file)
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        /// <summary>
        /// Проверяет, существует ли файл по заданному пути. Если существует - возращает массив байтов, если нет - null
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Файл в формате byte[]</returns>
        public static async Task<byte[]?> GetImage(string path)
        {
            var fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
            {
                return null;
            }

            var data = new byte[fileInfo.Length];

            await using var fileStream = fileInfo.OpenRead();
            var readAsync = await fileStream.ReadAsync(data);
            return data;
        }
    }
}
