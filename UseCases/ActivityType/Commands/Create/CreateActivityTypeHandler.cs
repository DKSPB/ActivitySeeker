namespace UseCases.ActivityType.Commands.Create
{
    using Models;
    using MediatR;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Repos;
    public class CreateActivityTypeHandler : IRequestHandler<CreateActivityTypeCommand, ActivityTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityTypeRepository _repository;

        public CreateActivityTypeHandler(IActivityTypeRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
    
        public async Task<ActivityTypeDto> Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ActivityType>(request);

            await _repository.CreateAsync(entity, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityTypeDto>(entity);
        }
    }
}