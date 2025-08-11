using MediatR;
using UseCases.Common;
using UseCases.Interfaces;
using DataAccess.Interfaces;
using UseCases.Activity.Queries.GetImage.Models;

namespace UseCases.Activity.Queries.GetImage;

public class GetImageHandler : IRequestHandler<GetImageCommand, FileResult?>
{
    private readonly IDbContext _context;
    private readonly IFileStorage _fileStorage;

    public GetImageHandler(IDbContext context, IFileStorage fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }
    
    public async Task<FileResult?> Handle(GetImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.ActivityId }, cancellationToken) ?? 
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);

        if (entity.ImageName is null) 
            throw new FileNotFoundException($"У активности {request.ActivityId} нет изображения");

        var imagePath = _fileStorage.GetImagePath(request.ImageSize);

        var imageFullPath = Path.Combine(imagePath, entity.ImageName);
        
        return entity.ImageName is null ? null : 
            new FileResult 
            { 
                Extension = Path.GetExtension(imageFullPath), 
                Content = await _fileStorage.GetAsync(imageFullPath) 
            };
    }
}