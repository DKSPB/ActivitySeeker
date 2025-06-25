using AutoMapper;
using DataAccess.Interfaces;
using MediatR;

namespace UseCases.Activity.Commands.Update;

public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _dbContext;

    public UpdateActivityHandler(IDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Activities
            .FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new  NullReferenceException($"Активность с идентификатором {request.Id} не найдена");
        }
        
        entity.ActivityTypeId = request.ActivityTypeId;
        entity.LinkOrDescription = request.LinkOrDescription;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Image = request.Image;
        entity.IsOnline = request.IsOnline;
        entity.CityId = request.CityId;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}