namespace UseCases.Activity.Commands.Update
{
    using AutoMapper;
    using MediatR;
    using Models;
    using Interfaces.Repos;
    using Domain.Entities;

    public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand, ActivityDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _repository;

        public UpdateActivityHandler(IMapper mapper, IActivityRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
    
        public async Task<ActivityDto> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.UpdateAsync(_mapper.Map<Activity>(request), cancellationToken);
        
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityDto>(entity);
        }
    }
}