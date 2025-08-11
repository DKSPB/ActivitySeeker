using MediatR;
using UseCases.Common;
using DataAccess.Interfaces;

namespace UseCases.Activity.Commands.Publish
{
    internal class PublishActivityHandler : IRequestHandler<PublishActivityCommand>
    {
        private readonly IDbContext _context;
        public PublishActivityHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PublishActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Activities.FindAsync(new object?[] { request.ActivityId }, cancellationToken) ??
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);

            entity.PublishActivity();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
