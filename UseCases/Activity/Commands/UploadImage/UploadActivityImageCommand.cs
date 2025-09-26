using MediatR;
using UseCases.Common;
using UseCases.Interfaces.Auth;

namespace UseCases.Activity.Commands.UploadImage;

/// <summary>
/// ���������� ����������� ��� ����������
/// </summary>
/// <param name="Id">������������� ����������</param>
/// <param name="InputFile">������ - �����������</param>
public record UploadActivityImageCommand(Guid Id, InputFile InputFile) : IRequest;