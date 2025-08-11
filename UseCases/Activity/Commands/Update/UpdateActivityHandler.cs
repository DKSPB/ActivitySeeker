using MediatR;
using UseCases.Common;
using DataAccess.Interfaces;
using Entities = Domain.Entities;

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
            .FindAsync(new object?[] { request.Id }, cancellationToken) ?? 
            throw new ObjectNotFoundException(nameof(Entities.Activity), request.Id);

        entity.ActivityTypeId = request.ActivityTypeId;
        entity.Description = request.Description;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Timezone = request.Timezone;
        entity.IsOnline = request.IsOnline;
        entity.CityId = request.CityId;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}