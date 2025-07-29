using MediatR;
using UseCases.Common;
using UseCases.User.Models;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UseCases.User.Queries.GetAll
{
    internal class GetUsersHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
    {
        private readonly IDbContext _contex;

        public GetUsersHandler(IDbContext context)
        {
            _contex = context;
        }
        public async Task<PagedResult<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var entities = _contex.Users;
            
            var total = await entities.CountAsync(cancellationToken);

            var items = await entities
                .Skip((query.Offset - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync(cancellationToken);

            return new PagedResult<UserDto>
            {
                Total = total,
                Items = items.Select(x => new UserDto { Id = x.Id }).ToList(),
            };
        }
    }
}
