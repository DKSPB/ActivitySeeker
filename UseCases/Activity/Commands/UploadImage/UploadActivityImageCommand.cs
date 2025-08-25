using MediatR;
using UseCases.Common;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.UploadImage;

/// <summary>
/// Добавление изображения для активности
/// </summary>
/// <param name="Id">Идентификатор активности</param>
/// <param name="InputFile">Объект - изображение</param>
public record UploadActivityImageCommand(Guid Id, InputFile InputFile) 
    : IRequest, IRequireOwnership<Domain.Entities.Activity, Guid>;