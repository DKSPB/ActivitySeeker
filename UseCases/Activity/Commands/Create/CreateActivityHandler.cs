namespace UseCases.Activity.Commands.Create
{
    using Models;
    using MediatR;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Repos;
    using UseCases.Interfaces.Common;
    
    internal class CreateActivityHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _repository;
        private readonly IDateTimeConverter _timeConverter;

        public CreateActivityHandler(IActivityRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IDateTimeConverter timeConverter)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _timeConverter = timeConverter;
        }
    
        public async Task<ActivityDto> Handle(CreateActivityCommand command, CancellationToken cancellationToken)
        {
            command.StartDate = _timeConverter.ToUtc(command.StartDate, command.Timezone).GetValueOrDefault();

            command.EndDate = _timeConverter.ToUtc(command.EndDate, command.Timezone);

            var entity = _mapper.Map<Activity>(command);
        
            await _repository.CreateAsync(entity, cancellationToken);
        
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityDto>(entity);
        }
    }
}

