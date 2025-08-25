using MediatR;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.Publish
{
    /// <summary>
    /// Публикация активности
    /// </summary>
    /// <param name="Id">Идентификатор активности</param>
    public record PublishActivityCommand(Guid Id) 
        : IRequest, IRequireOwnership<Domain.Entities.Activity, Guid>;
}
