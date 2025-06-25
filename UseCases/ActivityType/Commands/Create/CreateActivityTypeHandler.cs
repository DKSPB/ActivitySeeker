using MediatR;
using AutoMapper;
using DataAccess.Interfaces;
using ActivityTypeEntity = Domain.Entities.ActivityType;

namespace UseCases.ActivityType.Commands.Create;

public class CreateActivityTypeHandler : IRequestHandler<CreateActivityTypeCommand>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _dbContext;

    public CreateActivityTypeHandler(IDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.ActivityTypes.AddAsync(_mapper.Map<ActivityTypeEntity>(request), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}