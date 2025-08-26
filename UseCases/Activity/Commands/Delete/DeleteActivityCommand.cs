using MediatR;
using UseCases.Interfaces.Auth;

namespace UseCases.Activity.Commands.Delete;

/// <summary>
/// ������� �������� ����������
/// </summary>
/// <param name="Id">������������� ����������</param>
public record DeleteActivityCommand(Guid Id) : IRequest;