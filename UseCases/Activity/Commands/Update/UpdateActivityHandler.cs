using MediatR;
using DataAccess.Interfaces;

namespace UseCases.Activity.Commands.Update;

public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand>
{
    private readonly IDbContext _context;

    public UpdateActivityHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NullReferenceException($"Активность с идентификатором {request.Id} не найдена");
        }
        
        entity.ActivityTypeId = request.ActivityTypeId;
        entity.Description = request.Description;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Image = request.Image;
        entity.IsOnline = request.IsOnline;
        entity.CityId = request.CityId;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}