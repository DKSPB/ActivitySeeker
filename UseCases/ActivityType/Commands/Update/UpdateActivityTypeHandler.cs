using MediatR;
using DataAccess.Interfaces;
using UseCases.Common;

namespace UseCases.ActivityType.Commands.Update
{
    public class UpdateActivityTypeHandler : IRequestHandler<UpdateActivityTypeCommand>
    {
        private readonly IDbContext _context;
        public UpdateActivityTypeHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ActivityTypes
                .FindAsync(new object?[] { request.Id }, cancellationToken);

            if(entity is null)
            {
                throw new ObjectNotFoundException(nameof(Domain.Entities.ActivityType), request.Id);
            }
            
            if (request.ParentId is not null)
            {
                var parentEntity = await _context.ActivityTypes
                    .FindAsync(new object?[] { request.ParentId }, cancellationToken);

                if ( parentEntity is null) 
                    throw new ObjectNotFoundException(nameof(ActivityType), request.ParentId);
            }

            entity.TypeName = request.TypeName;
            entity.ParentId = request.ParentId;

            await _context.SaveChangesAsync(cancellationToken);
        }
        
    }
}
