using ActivitySeeker.Bll.Interfaces;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Bll.Services;

public class SettingsService : ISettingsService
{
    /// <summary>
    /// �������� ����� �� ��������� ����
    /// </summary>
    /// <param name="filePath">������ ���� � �����</param>
    /// <param name="file"> ���������� ����� (� ������)</param>
    /// <returns></returns>
    public async Task UploadImage(string filePath, Stream file)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    /// <summary>
    /// ��������� ����� � ������� ������ ������
    /// </summary>
    /// <param name="path">������ ���� � �����</param>
    /// <returns></returns>
    public async Task<byte[]> GetImage(string path)
    {
        var fileInfo = new FileInfo(path);

        var data = new byte[fileInfo.Length];

        await using var fileStream = fileInfo.OpenRead();
        var readAsync = await fileStream.ReadAsync(data);
        return data;
    }

    /// <summary>
    /// ������������� ���� � ������������ �� �������� ������������ � ���������� ����� ��������� webRootPath
    /// </summary>
    /// <param name="webRootPath">���������� ����� ���������, �������� � ���� ���� � �������</param>
    /// <returns></returns>
    public string CombinePathToFile(string webRootPath, string rootImageFolder, string fileName)
    {
        return Path.Combine(webRootPath, rootImageFolder, fileName);
    }
}