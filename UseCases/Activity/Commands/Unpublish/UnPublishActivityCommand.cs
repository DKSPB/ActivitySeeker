using MediatR;

namespace UseCases.Activity.Commands.UnPublish
{
    /// <summary>
    /// Снятие активности с публикации
    /// </summary>
    /// <param name="Id">Идентификатор активности</param>
    public record UnPublishActivityCommand(Guid Id) : IRequest;
}
