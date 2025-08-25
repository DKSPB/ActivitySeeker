using MediatR;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.Delete;

/// <summary>
/// Команда удаления активности
/// </summary>
/// <param name="Id">Идентификатор активности</param>
public record DeleteActivityCommand(Guid Id) 
    : IRequest, IRequireOwnership<Domain.Entities.Activity, Guid>;