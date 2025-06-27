using MediatR;
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
            var entities = _context.ActivityTypes.Where(x => request.ActivityTypeIds.Contains(x.Id));

            _context.ActivityTypes.RemoveRange(entities);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
