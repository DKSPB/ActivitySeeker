using AutoMapper;
using DataAccess.Interfaces;
using ActivityEntity = Domain.Entities.Activity;
using MediatR;

namespace UseCases.Activity.Commands.Create;

public class CreateActivityHandler : IRequestHandler<CreateActivityCommand>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _dbContext;

    public CreateActivityHandler(IDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Activities.AddAsync(_mapper.Map<ActivityEntity>(request), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}