using DataAccess.Interfaces;
using MediatR;
using UseCases.Common;
using UseCases.Interfaces;

namespace UseCases.Activity.Commands.UploadImage;

internal class UploadActivityImageHandler : IRequestHandler<UploadActivityImageCommand>
{
    private readonly IDbContext _context;
    private readonly IFileStorage _fileStorage;
    
    public UploadActivityImageHandler(IDbContext context, IFileStorage fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }
    public async Task Handle(UploadActivityImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.ActivityId }, cancellationToken);

        if (entity is null)
        {
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);
        }

        var fileName = await _fileStorage.SaveAsync(request.File.Content, request.File.FileExtension, cancellationToken);
        
        entity.ImagePath = fileName;

        await _context.SaveChangesAsync(cancellationToken);
    }
}