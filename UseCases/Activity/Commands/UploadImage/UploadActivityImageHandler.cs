using DataAccess.Interfaces;
using MediatR;
using UseCases.Common;
using DataAccess.Interfaces;

namespace UseCases.Activity.Commands.UploadImage;

internal class UploadActivityImageHandler : IRequestHandler<UploadActivityImageCommand>
{
    private readonly IDbContext _context;
    
    public UploadActivityImageHandler(IDbContext context)
    {
        _context = context;
    }
    public async Task Handle(UploadActivityImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.ActivityId }, cancellationToken);

        if (entity is null)
        {
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);
        }

        entity.ImagePath = request.FileName;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}