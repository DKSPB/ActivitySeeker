using ActivitySeeker.DataAccess.Interfaces.Infrastructure;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;

public class UpdateActivityTypeHandler : IRequestHandler<UpdateActivityTypeCommand>
{
    private readonly IDbContext _context;

    public UpdateActivityTypeHandler(IDbContext context)
    {
        _context = context;
    }
    
    public Task Handle(UpdateActivityTypeCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}