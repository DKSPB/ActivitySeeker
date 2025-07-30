using MediatR;

namespace UseCases.Activity.Commands.UploadImage;

public record UploadActivityImageCommand(Guid ActivityId, string FileName) : IRequest;