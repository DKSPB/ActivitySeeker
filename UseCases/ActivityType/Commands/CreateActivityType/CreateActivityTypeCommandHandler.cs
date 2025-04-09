
using ActivitySeeker.DataAccess.Interfaces.Infrastructure;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.CreateActivityType;

public class CreateActivityTypeCommandHandler : IRequestHandler<CreateActivityTypeCommand>
{
    private readonly IDbContext _context;
    
    public CreateActivityTypeCommandHandler(IDbContext dbContext)
    {
        _context = dbContext;
    }
    public async Task Handle(CreateActivityTypeCommand command, CancellationToken cancellationToken)
    {
        await _context.ActivityTypes.AddAsync(command.ActivityTypeDto.ToActivityType(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}