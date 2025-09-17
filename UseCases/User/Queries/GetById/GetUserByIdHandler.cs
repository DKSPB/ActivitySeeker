namespace UseCases.User.Queries.GetById
{
    using AutoMapper;
    using MediatR;
    using Models;
    using Interfaces.Repos;
    internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public GetUserByIdHandler(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _repository.GetByIdAsync(query.UserId, cancellationToken));
        }
    }
}
