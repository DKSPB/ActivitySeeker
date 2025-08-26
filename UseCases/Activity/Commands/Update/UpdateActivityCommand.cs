using UseCases.Activity.Commands.Create;
using UseCases.Interfaces.Auth;

namespace UseCases.Activity.Commands.Update;

/// <summary>
/// ���������� ����������
/// </summary>
public class UpdateActivityCommand : CreateActivityCommand
{
    /// <summary>
    /// ������������� ����������
    /// </summary>
    public Guid Id { get; set; }
}