using MediatR;
using AutoMapper;
using DataAccess.Interfaces;
using UseCases.Interfaces;
using ActivityEntity = Domain.Entities.Activity;


namespace UseCases.Activity.Commands.Create;

internal class CreateActivityHandler : IRequestHandler<CreateActivityCommand>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _dbContext;
    private readonly IDateTimeConverter _timeConverter;

    public CreateActivityHandler(IDbContext dbContext, IMapper mapper, IDateTimeConverter timeConverter)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _timeConverter = timeConverter;
    }
    
    public async Task Handle(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        command.StartDate = _timeConverter.ToUtc(command.StartDate, command.Timezone).GetValueOrDefault();

        command.EndDate = _timeConverter.ToUtc(command.EndDate, command.Timezone);

        await _dbContext.Activities.AddAsync(_mapper.Map<ActivityEntity>(command), cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}