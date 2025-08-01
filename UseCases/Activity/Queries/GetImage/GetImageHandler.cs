using DataAccess.Interfaces;
using MediatR;
using UseCases.Common;

namespace UseCases.Activity.Queries.GetImage;

public class GetImageHandler : IRequestHandler<GetImageCommand, FileStream>
{
    private readonly IDbContext _context;

    public GetImageHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<FileStream> Handle(GetImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.ActivityId }, cancellationToken);

        if (entity is null)
        {
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);
        }
        
        
    }
}