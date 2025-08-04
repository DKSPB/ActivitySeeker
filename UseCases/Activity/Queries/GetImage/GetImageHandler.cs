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


        return entity.ImagePath is null ? null : 
            new FileResult 
            { 
                Extension = Path.GetExtension(entity.ImagePath), 
                Content = await _fileStorage.GetAsync(entity.ImagePath) 
            };
    }
}