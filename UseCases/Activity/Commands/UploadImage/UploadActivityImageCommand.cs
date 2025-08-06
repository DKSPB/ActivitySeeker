using MediatR;
using UseCases.Common;

namespace UseCases.Activity.Commands.UploadImage;

public record UploadActivityImageCommand(Guid ActivityId, InputFile InputFile) : IRequest;