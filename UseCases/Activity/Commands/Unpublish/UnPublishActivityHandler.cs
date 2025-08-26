using DataAccess.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Common;

namespace UseCases.Activity.Commands.UnPublish
{
    internal class UnPublishActivityHandler : IRequestHandler<UnPublishActivityCommand>
    {
        private readonly IDbContext _context;
        public UnPublishActivityHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UnPublishActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Activities.FindAsync(new object?[] { request.Id }, cancellationToken) ??
                throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.Id);

            entity.UnpublishActivity();

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
