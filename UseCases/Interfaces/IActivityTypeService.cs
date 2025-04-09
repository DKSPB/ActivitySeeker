using ActivitySeeker.UseCases.ActivityType.Dto;
using ActivitySeeker.UseCases.Models;

namespace ActivitySeeker.UseCases.Interfaces;

public interface IActivityTypeService
{
    /// <summary>
    /// ��������� ������ ����� �����������
    /// </summary>
    /// <returns></returns>
    Task<List<ActivityTypeDto>> GetAll();

    /// <summary>
    /// ��������� ���� ����������� �� ������������
    /// </summary>
    /// <param name="id">������������� ���� ����������</param>
    /// <returns></returns>
    Task<ActivityTypeDto> GetById(Guid id);

    /// <summary>
    /// �������� ����� ����������
    /// </summary>
    /// <param name="activityType">������ - ��� ����������</param>
    /// <returns></returns>
    Task Create(ActivityTypeDto activityType);

    /// <summary>
    /// ��������� ���� ����������
    /// </summary>
    /// <param name="activityType">������ - ��� ����������</param>
    /// <returns></returns>
    Task Update(ActivityTypeDto activityType);

    /// <summary>
    /// �������� ����� ����������� �� ���������������
    /// </summary>
    /// <param name="activityTypeIds">������ ��������������� ����� �����������</param>
    /// <returns></returns>
    Task Delete(List<Guid> activityTypeIds);

    /// <summary>
    /// �������� ����������� ��� ���������� ���� ����������
    /// </summary>
    /// <param name="activityTypeId">������������� ����������</param>
    /// <param name="path">������ ���� � �����</param>
    /// <param name="image">�������</param>
    /// <returns></returns>
    Task UploadImage(Guid activityTypeId, string path, Stream image);
}