using MediatR;
using UseCases.Common;
using DataAccess.Interfaces;

namespace UseCases.ActivityType.Commands.Delete
{
    internal class DeleteActivityTypeHandler : IRequestHandler<DeleteActivityTypeCommand>
    {
        private readonly IDbContext _context;

        public DeleteActivityTypeHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteActivityTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ActivityTypes
                .FindAsync(new object?[] { request.ActivityTypeId }, cancellationToken: cancellationToken) ??
                throw new ObjectNotFoundException(nameof(Domain.Entities.ActivityType), request.ActivityTypeId);

            _context.ActivityTypes.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
