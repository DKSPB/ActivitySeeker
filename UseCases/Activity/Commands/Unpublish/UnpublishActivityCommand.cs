using MediatR;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.Unpublish
{
    /// <summary>
    /// Снятие активности с публикации
    /// </summary>
    /// <param name="Id">Идентификатор активности</param>
    public record UnpublishActivityCommand(Guid Id) 
        : IRequest, IRequireOwnership<Domain.Entities.Activity, Guid>;
}
