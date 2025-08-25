using UseCases.Activity.Commands.Create;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.Update;

/// <summary>
/// Обновление активности
/// </summary>
public class UpdateActivityCommand 
    : CreateActivityCommand, IRequireOwnership<Domain.Entities.Activity, Guid>
{
    /// <summary>
    /// Идентификатор активности
    /// </summary>
    public Guid Id { get; set; }
}