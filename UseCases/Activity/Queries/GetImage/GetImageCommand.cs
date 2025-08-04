using MediatR;
using UseCases.Activity.Queries.GetImage.Models;
using UseCases.Common;

namespace UseCases.Activity.Queries.GetImage;

public record GetImageCommand(Guid ActivityId) : IRequest<FileResult?>;