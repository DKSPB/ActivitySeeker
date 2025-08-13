using MediatR;
using DataAccess.Interfaces;
using UseCases.Common;

namespace UseCases.Activity.Commands.Delete;

public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand>
{
    private readonly IDbContext _context;

    public DeleteActivityHandler(IDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.ActivityId }, cancellationToken: cancellationToken) ??
                       throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);

        _context.Activities.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}