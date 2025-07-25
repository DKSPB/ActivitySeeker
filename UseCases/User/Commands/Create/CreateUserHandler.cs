using DataAccess.Interfaces;
using MediatR;
using UseCases.User.Models;
using Entity = Domain.Entities;

namespace UseCases.User.Commands.Create
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IDbContext _context;
        public CreateUserHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(new Entity.User { Id = command.Id}, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
