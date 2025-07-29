using MediatR;
using UseCases.Common;
using UseCases.User.Models;
using DataAccess.Interfaces;
using User = Domain.Entities.User;

namespace UseCases.User.Queries.GetById
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IDbContext _context;

        public GetUserByIdHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .FindAsync(new object?[] { query.UserId }, cancellationToken: cancellationToken);

            return entity is null
                ? throw new ObjectNotFoundException(nameof(User), query.UserId)
                : new UserDto { Id = entity.Id };
        }
    }
}
