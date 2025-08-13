using MediatR;
using AutoMapper;
using DataAccess.Interfaces;
using UseCases.Activity.Models;
using UseCases.Interfaces;
using ActivityEntity = Domain.Entities.Activity;


namespace UseCases.Activity.Commands.Create;

internal class CreateActivityHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
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
    
    public async Task<ActivityDto> Handle(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        command.StartDate = _timeConverter.ToUtc(command.StartDate, command.Timezone).GetValueOrDefault();

        command.EndDate = _timeConverter.ToUtc(command.EndDate, command.Timezone);

        var entity = _mapper.Map<ActivityEntity>(command);
        
        await _dbContext.Activities.AddAsync(entity, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ActivityDto>(entity);
    }
}