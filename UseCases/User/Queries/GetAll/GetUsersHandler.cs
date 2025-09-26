namespace UseCases.User.Queries.GetAll
{
    using Common;
    using Models;
    using MediatR;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Repos;
    internal class GetUsersHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public GetUsersHandler(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PagedResult<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var pagingSpecification = new PagingSpecification<User>(query.Limit, query.Offset);

            var result = await _repository.GetAllAsync(pagingSpecification, cancellationToken);

            return new PagedResult<UserDto>
            {
                Total = result.Total,
                Items = _mapper.Map<List<UserDto>>(result.Items)
            };
        }
    }
}
