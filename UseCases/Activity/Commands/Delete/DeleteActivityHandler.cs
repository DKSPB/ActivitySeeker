using MediatR;
using DataAccess.Interfaces;

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
        var entities = _context.Activities.Where(x => request.ActivityIds.Contains(x.Id));

        _context.Activities.RemoveRange(entities);

        await _context.SaveChangesAsync(cancellationToken);
    }
}