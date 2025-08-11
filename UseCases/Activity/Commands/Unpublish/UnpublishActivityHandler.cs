using DataAccess.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Common;

namespace UseCases.Activity.Commands.Unpublish
{
    internal class UnpublishActivityHandler : IRequestHandler<UnpublishActivityCommand>
    {
        private readonly IDbContext _context;
        public UnpublishActivityHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UnpublishActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Activities.FindAsync(new object?[] { request.ActivityId }, cancellationToken) ??
                throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);

            entity.UnpublishActivity();

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
