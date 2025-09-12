namespace UseCases.ActivityType.Queries.GetById
{
    using Models;
    using MediatR;
    using AutoMapper;
    using Interfaces.Repos;
    internal class GetActivityTypeByIdHandler : IRequestHandler<GetActivityTypeByIdQuery, ActivityTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IActivityTypeRepository _repository;
        public GetActivityTypeByIdHandler(IMapper mapper, IActivityTypeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ActivityTypeDto> Handle(GetActivityTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            
            return _mapper.Map<ActivityTypeDto>(entity);
        }
    }
}
