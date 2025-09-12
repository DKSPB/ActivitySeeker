namespace UseCases.Activity.Queries.GetById
{
    using MediatR;
    using AutoMapper;
    using Models;
    using Interfaces.Repos;
    internal class GetActivityByIdHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _repository;
        public GetActivityByIdHandler(IMapper mapper, IActivityRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ActivityDto> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            
            return _mapper.Map<ActivityDto>(entity);
        }
    }
}
