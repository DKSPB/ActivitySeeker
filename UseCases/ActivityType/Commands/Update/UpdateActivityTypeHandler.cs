namespace UseCases.ActivityType.Commands.Update
{
    using MediatR;
    using Models;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Repos;
    public class UpdateActivityTypeHandler : IRequestHandler<UpdateActivityTypeCommand, ActivityTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityTypeRepository _repository;
        public UpdateActivityTypeHandler(IMapper mapper, IUnitOfWork unitOfWork, IActivityTypeRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<ActivityTypeDto> Handle(UpdateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.UpdateAsync(_mapper.Map<ActivityType>(request), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityTypeDto>(entity);
        }
    }
}
