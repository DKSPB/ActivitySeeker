using MediatR;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.UnPublish
{
    /// <summary>
    /// Снятие активности с публикации
    /// </summary>
    /// <param name="Id">Идентификатор активности</param>
    public record UnPublishActivityCommand(Guid Id) 
        : IRequest, IRequireOwnership<Domain.Entities.Activity, Guid>;
}
