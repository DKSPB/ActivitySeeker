using MediatR;

namespace UseCases.Activity.Queries.GetImage;

public record GetImageCommand(Guid ActivityId) : IRequest<FileStream>;