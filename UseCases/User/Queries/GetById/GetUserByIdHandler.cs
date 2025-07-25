using DataAccess.Interfaces;
using MediatR;
using UseCases.User.Models;

namespace UseCases.User.Queries.GetById
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery>
    {
        private readonly IDbContext _context;

        public GetUserByIdHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var userEntity = await _context.Users
                .FindAsync(new object?[] { query.UserId }, cancellationToken: cancellationToken);
            
        }
    }
}
