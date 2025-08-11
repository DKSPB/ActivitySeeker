using MediatR;
using AutoMapper;
using DataAccess.Interfaces;
using ActivityTypeEntity = Domain.Entities.ActivityType;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Commands.Create;

public class CreateActivityTypeHandler : IRequestHandler<CreateActivityTypeCommand, ActivityTypeDto>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _dbContext;

    public CreateActivityTypeHandler(IDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<ActivityTypeDto> Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ActivityTypeEntity>(request);

        await _dbContext.ActivityTypes.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ActivityTypeDto>(entity);
    }
}