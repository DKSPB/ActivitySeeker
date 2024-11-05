using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Bll.Interfaces;

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
}